# Kaleidoscope
Made with Unity3D.

![](https://www.safacon.com/site/images/kaleidoscope.gif)

Contains 3 designs, all procedurally generated.
See Basic.cs & Equilateral.cs for completed designs. (Octagons.cs is currently incomplete)

Consists of two isometric cameras.

### Capture Cam
Watches a generated square that renders the device/web cam contents using a [WebCamTexture](https://docs.unity3d.com/ScriptReference/WebCamTexture.html)


### Main Camera
Watches the wall of polygons that were generated based on the design. Each polygon is rendering the contents of the Capture Cam using a [Render Texture](https://docs.unity3d.com/Manual/class-RenderTexture.html).


