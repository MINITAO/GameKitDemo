using UnityEngine;
using System.Collections;
///
/// <summary>
/// 需要一个主画布和一个临时画布，在绘制过程中，主画布为擦除模式，临时画布先绘制，当鼠标抬起时，
/// 再将临时画布中的图像绘制到主画布，最后清空临时画布。
/// 
/// 注意： 	1.  临时画布的画笔最好比主画布的画笔大一点点，否则边缘可能有空隙
/// 		2. 主画布的paintType为None
/// </summary>
/// 
public class MakeupTest : MonoBehaviour {
	//主画布
	public RenderTexturePainter paintCanvas;
	//临时画布，显示层级比主画布高一点点
	public RenderTexturePainter tempPaintCanvas;

	//多次涂抹的图片是否是一样的大小，一样的透明区域，如果不一样，则paintCanvas要执行擦除动画
	public bool isDifferent = true;

	// Use this for initialization
	void Start () {
		if(isDifferent){
			//临时画布的画笔最好比主画布的画笔大一点点
			paintCanvas.brushScale = tempPaintCanvas.brushScale*0.92f;
		}
		tempPaintCanvas.gameObject.SetActive(false);
	}
	

	void Update(){
		if(InputUtil.CheckMouseOnUI()) return;

		//下面是两种涂法，两种都可以使用，只是有一些区域
		//第一种是每次都会将临时画布的结果Draw到最终画布上，效率低一些，但是涂抹过程中效果好一些，适用于要涂抹的对象透明度和区域一不样的情况
		//第二种是在最后才把涂抹的结果Draw到最终画布，效率要高一些

		if(isDifferent)
		{
			if(Input.GetMouseButtonDown(0)){
				tempPaintCanvas.gameObject.SetActive(true);
			}
			else if(Input.GetMouseButton(0)){
				paintCanvas.isErase = true;
				tempPaintCanvas.canvasMat.SetFloat("_BlendSrc",(int)UnityEngine.Rendering.BlendMode.SrcAlpha);
				tempPaintCanvas.canvasMat.SetFloat("_BlendDst",(int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				tempPaintCanvas.Drawing(Input.mousePosition);
				if(isDifferent){
					paintCanvas.Drawing(Input.mousePosition);
				}

				tempPaintCanvas.canvasMat.SetFloat("_BlendSrc",(int)UnityEngine.Rendering.BlendMode.One);
				tempPaintCanvas.canvasMat.SetFloat("_BlendDst",(int)UnityEngine.Rendering.BlendMode.Zero);
				//draw temp paint canvas to paint canvas
				paintCanvas.isErase = false;
				paintCanvas.ClickDraw(Camera.main.WorldToScreenPoint(tempPaintCanvas.transform.position),
					Camera.main,paintCanvas.renderTexture,1f,tempPaintCanvas.canvasMat);
				//clear temp canvas
				tempPaintCanvas.ClearCanvas();
			}
			else if(Input.GetMouseButtonUp(0)){
				tempPaintCanvas.EndDraw();
				paintCanvas.EndDraw();
				tempPaintCanvas.gameObject.SetActive(false);
			}
		}
		else
		{

			if(Input.GetMouseButtonDown(0)){
				if(isDifferent){
					//主画布当前为擦除模式
					paintCanvas.isErase = true;
				}
				tempPaintCanvas.gameObject.SetActive(true);
				tempPaintCanvas.canvasMat.SetFloat("_BlendSrc",(int)UnityEngine.Rendering.BlendMode.SrcAlpha);
				tempPaintCanvas.canvasMat.SetFloat("_BlendDst",(int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			}
			else if(Input.GetMouseButton(0)){
				if(isDifferent){
					paintCanvas.Drawing(Input.mousePosition);
				}
				tempPaintCanvas.Drawing(Input.mousePosition);
			}
			else if(Input.GetMouseButtonUp(0)){
				paintCanvas.EndDraw();
				tempPaintCanvas.EndDraw();

				tempPaintCanvas.canvasMat.SetFloat("_BlendSrc",(int)UnityEngine.Rendering.BlendMode.One);
				tempPaintCanvas.canvasMat.SetFloat("_BlendDst",(int)UnityEngine.Rendering.BlendMode.Zero);

				//将tempPaintCanvas画的draw到paintCavas上
				paintCanvas.isErase = false;
				paintCanvas.ClickDraw(Camera.main.WorldToScreenPoint(tempPaintCanvas.transform.position),
					Camera.main,paintCanvas.renderTexture,1f,tempPaintCanvas.canvasMat);
				//清空临时画布
				tempPaintCanvas.ClearCanvas();
				tempPaintCanvas.gameObject.SetActive(false);
			}
		}
	}


	public void ChangeTexture( Texture2D t){
		paintCanvas.sourceTexture = t;
		tempPaintCanvas.sourceTexture = t;
	}
}
