using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Object = UnityEngine.Object;

// levelString is a string of 0s and 1s, with 0s being spikes and 1s being platforms
// first characters are a|b|c| where a is an integer representing the width, b, height, and c, length of the level
// a b and c can be any integer from 0 to 100
// then the rest of the string is the level data formatted such that:
// each platform object is represented by a 1 followed by "[x,y,z,length,offset]" where x, y, and z are integers representing the position of the platform
// each spike object is represented by a 0 followed by "[x,y,z,length,offset]" where x, y, and z are integers representing the position of the spike
// and where length is the length of the platform and offset is the offset of the platform from the origin
// followed by a . and then the next platform object
// example a 3x3x3 cube with a 1x1x1 platform in the center would be represented as "3|3|3|1[1,1,1,1,0]." 
// example a 2x2x2 cube of platforms would be represented as "2|2|2|1[0,0,0,1,0].1[1,0,0,1,0].1[0,1,0,1,0].1[1,1,0,1,0].1[0,0,1,1,0].1[1,0,1,1,0].1[0,1,1,1,0].1[1,1,1,1,0]."
// example a 2x2x2 cube of spikes would be represented as "2|2|2|0[0,0,0,1,0].0[1,0,0,1,0].0[0,1,0,1,0].0[1,1,0,1,0].0[0,0,1,1,0].0[1,0,1,1,0].0[0,1,1,1,0].0[1,1,1,1,0]."
// the last character is a period, which is ignored
//
//COINS: use 2 followed by [x,y,z,a,b] for coins. Values a, b are ignored but still need to be present for formatting.
public class Level {
    private readonly List<int[]> coinData;
    
    private readonly int levelWidth;
    private readonly int levelHeight;
    private readonly int levelLength;
    
    private readonly Material platform1;
    private readonly Material platform2;
    
    //prefabs
    private readonly GameObject spike;
    private readonly GameObject coin;
    private readonly GameObject winZone;

    private Level(int w, int h, int l, List<int[]> platformData, List<int[]> spikeData, List<int[]> coinData) {
        spike = Resources.Load<GameObject>("Prefabs/Spike");
        coin = Resources.Load<GameObject>("Prefabs/Coin");
        winZone = Resources.Load<GameObject>("Prefabs/WinZone");
        
        platform1 = Resources.Load<Material>("Materials/Platform1");
        platform2 = Resources.Load<Material>("Materials/Platform2");
        
        levelWidth = w;
        levelHeight = h;
        levelLength = l;
        
        this.coinData = coinData;
        
        foreach (var p in platformData) {
            int x = p[0];
            int y = p[1];
            int z = p[2];
            int length = p[3];
            int offset = p[4];
            int version = p[5];
            CreatePlatform(x, y, z, length, offset, version);
        }
        
        foreach (var s in spikeData) {
            int x = s[0];
            int y = s[1];
            int z = s[2];
            int length = s[3];
            int halfLength = length / 2;
            for(int j = 0; j < length; j++) {
                CreateSpike(x, y, z + j - halfLength);
            }
        }
        
        foreach (var c in coinData) {
            int x = c[0];
            int y = c[1];
            int z = c[2];
            CreateCoin(x, y, z);
        }
        
        CreateWinZone();
    }
    
    private void CreatePlatform(int x, int y, int z, int length, int offset, int version) {
        switch (version) {
            case 1 when platform1 != null: {
                GameObject p1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                p1.transform.position = new Vector3(x, y, z);
                p1.transform.localScale = new Vector3(1, 1, length);
                p1.GetComponent<Renderer>().material = platform1;
                p1.AddComponent<PlatformController>().startZ = offset;
                break;
            }
            case 2 when platform2 != null: {
                GameObject p2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                p2.transform.position = new Vector3(x, y, z);
                p2.transform.localScale = new Vector3(1, 1, length);
                p2.GetComponent<Renderer>().material = platform2;
                p2.AddComponent<PlatformController>().startZ = offset;
                break;
            }
        }
    }

    private void CreateSpike(int x, int y, int z) {
        if (spike != null) Object.Instantiate(spike, new Vector3(x, y - 0.16f, z), Quaternion.identity);
    }

    private void CreateCoin(int x, int y, int z) {
        if (coin != null) Object.Instantiate(coin, new Vector3(x, y, z), Quaternion.identity);
    }

    private void CreateWinZone() {
        if(winZone != null) {
            GameObject obj = Object.Instantiate(winZone, new Vector3(levelLength, levelHeight / 2.0f, 0), Quaternion.identity);
            obj.transform.localScale = new Vector3(1,levelHeight,levelWidth);
        }
    }

    public void RegenerateCoins() {
        foreach (GameObject c in GameObject.FindGameObjectsWithTag("Coin")) {
            Object.Destroy(c);
        }
        foreach (var c in coinData) {
            int x = c[0];
            int y = c[1];
            int z = c[2];
            CreateCoin(x, y, z);
        }
    }

    public class Builder {
        private readonly int width;
        private readonly int height;
        private readonly int length;
        
        private readonly List<int[]> platformData;
        private readonly List<int[]> spikeData;
        private readonly List<int[]> coinData;
        
        public Builder(int width, int height, int length) {
            this.width = width;
            this.height = height;
            this.length = length;
            platformData = new List<int[]>();
            spikeData = new List<int[]>();
            coinData = new List<int[]>();
        }

        public Builder P(int x, int y, int z, int l, int offset, int version) {
            platformData.Add(new[] {x, y, z, l, offset, version});
            return this;
        }
        
        public Builder S(int x, int y, int z, int l) {
            spikeData.Add(new[] {x, y, z, l});
            return this;
        }
        
        public Builder C(int x, int y, int z) {
            coinData.Add(new[] {x, y, z});
            return this;
        }

        public Level Build() {
            return new Level(width, height, length, platformData, spikeData, coinData);
        }
    }
}