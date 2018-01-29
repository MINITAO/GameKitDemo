#注意查看Demo中的Readme

1. Bone2D: DragonBones和Spine转成Unity原生动画
2. Event: 全局事件
3. Filter2D: Sprite 和 UGUI 常用的一些滤镜
4. ImageProcess: 图片效果滤镜
5. Painter: 涂抹，画线工具
6. Particle2D:  2D粒子，支持UGUI，支持cocos2dx,Starling 等2d引擎粒子转到Unity中

## Sprite
7. SpriteMask: Sprite遮罩
8. SpriteMoveRotateScale: 对Sprite进行旋转、移动、缩放
9. SpriteDrager: 对Sprite进行拖动
10. SpriteLayout: Sprite在屏幕中的对齐方式，适配等
11. SpriteTouch: Sprite点击处理

## UGUI
12. UGUIDrager: 对UGUI对象进行拖动处理
13. Joystick
14. BitmapFont
15. CenterView 组件
16. PageView 组件，支持循环
17. LoopScrollView: 支持循环
18. UIMask: 通过一张贴图来实现UI的遮罩
19. UIPolygon: 设置UI的点击区域为自定义多边形
20. ExtendImage: 通过缩小图片来达到放大点击区域
21. USprite/USpriteUGUI: 对Sprite或Image进行扭曲和透视

## Util
22. AudioManager: 声音管理
23. ResManager: 资源管理 ，主要是对Resources和StreamingAssets中的图片进行加载，缓存等
24. DynamicAsset: 在Unity中显示StreamingAssets中的图片
25. SpriteSplitAlpha: 对图片分离为RGB+A
26. PngProcess: 对图片进行加密，预乘Alpha处理
27. TexturePackEditr: 对Spawing/Starling 格式的图集转成Unity Sprite; 将散图合并为图集
28. InputUtil 用于判断当前点击是否在UGUI上
29. CSV, PlayerPrefsX, BesizerUtil, MathUtil, Matrix2D, FSPCouter等

## Dragonbones2SpineData
可以将DragonBones 数据转成 Spine数据