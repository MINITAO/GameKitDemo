# 注意
1. 画线和涂抹用的是不同的Shader

2. Use Vector Graphic(使用矢量图) 性能高，但不支持半透明画笔。如果在做擦除时，想到慢慢渐隐擦除的效果，
这里就能使用矢量方式。

3. 如果要在不规则的物体上画图，只需要在Mask Texture处加一张遮罩图就行了。


# API
//用在类似mouse move中
void Drawing(Vector3 screenPos , Camera camera=null); 

//用在类似mouse up中
void EndDraw();

//将贴图画到RenderTexture上，如果参数中默认为null的地方会参时设置为null,则用使用当前RenderTexturePainter中的默认参数代替
void ClickDraw(Vector3 screenPos , Camera camera=null , Texture pen=null, 
				float penScale=1f , Material drawMat = null , RenderTexture rt=null);