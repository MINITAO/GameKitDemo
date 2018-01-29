# Bitmap Font 位图字体
使用方法：1. 在位图字体制作软件中，导出时选择fnt(xml)格式
2. 将导出的png和fnt放入unity中，选中fnt文件，点鼠标右键，选择Generate Bitmap Font



# UI Polygon
主要用于UI是不规则的点击区域，通过Collider2D来实现的不规则区域



# UI Mask By Texture
用于处理在UI中用图片进行遮罩，使用方式和SpriteMask差不多。


# UGUI Drager
 -UI拖动，UI上面需要有类似Image的组件，并且Raycast Target要设置为True才能拖动
 -触发类型有点，圆，矩形，触发对象需要有Collider2D组件，并且触发对象的z最好比拖动对象的z值大，即拖动对象靠近相机一些。
 -设置触发对象的Layer Mask，即'Drop Ray Cast Mask'参数