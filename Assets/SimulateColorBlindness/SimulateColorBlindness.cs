/*FILE INFO:
	Filename    : SimulateColorBlindness.cs
	Last Updated: November 20th, 2013
	Version     : 1.1
*/

/* LICENSE:
	The MIT License (MIT)
	
	Copyright (c) 2013 Drash
	
	Permission is hereby granted, free of charge, to any person obtaining a copy
	of this software and associated documentation files (the "Software"), to deal
	in the Software without restriction, including without limitation the rights
	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
	copies of the Software, and to permit persons to whom the Software is
	furnished to do so, subject to the following conditions:
	
	The above copyright notice and this permission notice shall be included in
	all copies or substantial portions of the Software.
	
	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
	THE SOFTWARE.
*/

/*NOTE:
	All of the color transforms are derived from the work done by the kind person that posted them at
		http://web.archive.org/web/20081014161121/http://www.colorjack.com/labs/colormatrix/
	As such, there is no guarantee that these colorblindness simulations are accurate!
*/

/*USAGE:
	Drop this script onto a Camera and configure which type of colorblindness you want to see.
	You may additionally specify a key to use for cycling between all the colorblindness types at runtime.
*/

using UnityEngine;

public class SimulateColorBlindness : MonoBehaviour 
{
	public enum ColorBlindnessType
	{
		NormalVision = 0,
		Protanopia = 1,
		Protanomaly = 2,
		Deuteranopia = 3,
		Deuteranomaly = 4,
		Tritanopia = 5,
		Tritanomaly = 6,
		Achromatopsia = 7,
		Achromanomaly = 8
	}
	
	[SerializeField]
	private ColorBlindnessType type = ColorBlindnessType.NormalVision;
	public ColorBlindnessType colorBlindnessType
	{
		set
		{
			if(type != value)
			{
				type = value;
				UpdateMaterial();
			}
		}
		get
		{
			return type;
		}
	}
	
	[SerializeField]
	public KeyCode keyToCycleType = KeyCode.None;
	
	public Material material;
	
	void Start()
	{
		UpdateMaterial();
	}
	
	void Update()
	{
		if(keyToCycleType != KeyCode.None && Input.GetKeyDown(keyToCycleType))
			colorBlindnessType = (ColorBlindnessType)(((int)colorBlindnessType + 1) % 9);
	}
	
	void OnRenderImage(RenderTexture source, RenderTexture destination) 
	{
		if(material != null)
    		Graphics.Blit(source, destination, material);
	}
	
	private void UpdateMaterial()
	{
		if(material == null)
			return;
		
		switch(type)
		{
			default:
			case ColorBlindnessType.NormalVision:
				Matrix4x4 normalMatrix = Matrix4x4.identity;
				material.SetMatrix("_colorTransform", normalMatrix);
				break;
			case ColorBlindnessType.Protanopia:
				Matrix4x4 protanopiaMatrix = Matrix4x4.identity;
				protanopiaMatrix.SetColumn(0, new Vector4(0.567f, 0.433f,     0f, 0f));
				protanopiaMatrix.SetColumn(1, new Vector4(0.558f, 0.442f,     0f, 0f));
				protanopiaMatrix.SetColumn(2, new Vector4(    0f, 0.242f, 0.758f, 0f));
				protanopiaMatrix.SetColumn(3, new Vector4(0f,         0f,     0f, 1f));
				material.SetMatrix("_colorTransform", protanopiaMatrix);
				break;
			case ColorBlindnessType.Tritanopia:
				Matrix4x4 tritanopiaMatrix = Matrix4x4.identity;
				tritanopiaMatrix.SetColumn(0, new Vector4(0.95f,  0.05f,     0f, 0f));
				tritanopiaMatrix.SetColumn(1, new Vector4(0f,    0.433f, 0.567f, 0f));
				tritanopiaMatrix.SetColumn(2, new Vector4(0f,    0.475f, 0.525f, 0f));
				tritanopiaMatrix.SetColumn(3, new Vector4(0f,        0f,     0f, 1f));
				material.SetMatrix("_colorTransform", tritanopiaMatrix);
				break;
			case ColorBlindnessType.Deuteranopia:
				Matrix4x4 deuteranopiaMatrix = Matrix4x4.identity;
				deuteranopiaMatrix.SetColumn(0, new Vector4(0.625f, 0.375f,   0f, 0f));
				deuteranopiaMatrix.SetColumn(1, new Vector4(0.7f,     0.3f,   0f, 0f));
				deuteranopiaMatrix.SetColumn(2, new Vector4(0f,       0.3f, 0.7f, 0f));
				deuteranopiaMatrix.SetColumn(3, new Vector4(0f,         0f,   0f, 1f));
				material.SetMatrix("_colorTransform", deuteranopiaMatrix);
				break;
			case ColorBlindnessType.Protanomaly:
				Matrix4x4 protanomalyMatrix = Matrix4x4.identity;
				protanomalyMatrix.SetColumn(0, new Vector4(0.817f, 0.183f,     0f, 0f));
				protanomalyMatrix.SetColumn(1, new Vector4(0.333f, 0.667f,     0f, 0f));
				protanomalyMatrix.SetColumn(2, new Vector4(0f,     0.125f, 0.875f, 0f));
				protanomalyMatrix.SetColumn(3, new Vector4(0f,         0f,     0f, 1f));
				material.SetMatrix("_colorTransform", protanomalyMatrix);
				break;
			case ColorBlindnessType.Deuteranomaly:
				Matrix4x4 deuteranomalyMatrix = Matrix4x4.identity;
				deuteranomalyMatrix.SetColumn(0, new Vector4(0.8f,     0.2f,     0f, 0f));
				deuteranomalyMatrix.SetColumn(1, new Vector4(0.258f, 0.742f,     0f, 0f));
				deuteranomalyMatrix.SetColumn(2, new Vector4(0f,     0.142f, 0.858f, 0f));
				deuteranomalyMatrix.SetColumn(3, new Vector4(0f,         0f,     0f, 1f));
				material.SetMatrix("_colorTransform", deuteranomalyMatrix);
				break;
			case ColorBlindnessType.Tritanomaly:
				Matrix4x4 tritanomalyMatrix = Matrix4x4.identity;
				tritanomalyMatrix.SetColumn(0, new Vector4(0.967f, 0.033f,     0f, 0f));
				tritanomalyMatrix.SetColumn(1, new Vector4(0f,     0.733f, 0.267f, 0f));
				tritanomalyMatrix.SetColumn(2, new Vector4(0f,     0.183f, 0.817f, 0f));
				tritanomalyMatrix.SetColumn(3, new Vector4(0f,         0f,     0f, 1f));
				material.SetMatrix("_colorTransform", tritanomalyMatrix);
				break;
			case ColorBlindnessType.Achromatopsia:
				Matrix4x4 achromatopsiaMatrix = Matrix4x4.identity;
				achromatopsiaMatrix.SetColumn(0, new Vector4(0.299f, 0.587f, 0.114f, 0f));
				achromatopsiaMatrix.SetColumn(1, new Vector4(0.299f, 0.587f, 0.114f, 0f));
				achromatopsiaMatrix.SetColumn(2, new Vector4(0.299f, 0.587f, 0.114f, 0f));
				achromatopsiaMatrix.SetColumn(3, new Vector4(0f,         0f,     0f, 1f));
				material.SetMatrix("_colorTransform", achromatopsiaMatrix);
				break;
			case ColorBlindnessType.Achromanomaly:
				Matrix4x4 achromanomalyMatrix = Matrix4x4.identity;
				achromanomalyMatrix.SetColumn(0, new Vector4(0.618f, 0.320f, 0.062f, 0f));
				achromanomalyMatrix.SetColumn(1, new Vector4(0.163f, 0.775f, 0.062f, 0f));
				achromanomalyMatrix.SetColumn(2, new Vector4(0.163f, 0.320f, 0.516f, 0f));
				achromanomalyMatrix.SetColumn(3, new Vector4(0f,         0f,     0f, 1f));
				material.SetMatrix("_colorTransform", achromanomalyMatrix);
				break;
		}
		
		Debug.Log("Currently simulating '" + type.ToString() + "'.");
	}

	public void SetMode(int index) {
		colorBlindnessType = (ColorBlindnessType)index;
	}
}