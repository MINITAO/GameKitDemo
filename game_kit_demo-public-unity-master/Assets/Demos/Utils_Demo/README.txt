# Premulity Alpha
此方法可解决的问题，动态从StreamingAsset加载图片显示出现 黑边或白边
使用方法：选中图片或装有图片的文件夹，然后点击Unity菜单栏中Tools/Premulity Alpha(图片/文件夹)


# Split Alpha
将图片转成RGB+A 两张或一张jpg图 。 此方法可解决的问题：

-因为PVRTC在ios上质量比较差，而jpg压缩后质量下降相对没那么厉害。
-某些老的Android不支持ETC2格式，如果使用了ETC2，则在这些机器上会先解压成RGBA32。而ECT1不支持透明通道，
所以转成jpg后就可以支持ETC1格式，在老机器上加载会快很多。

- 在ios上还可以用ASTC格式，但是低端的ios不支持ASTC，会自动解压成RGBA32，解压过程会消耗一定的CPU和内存


# Texture Packer Editor
-可以将散图打包成图集，不过建议使用TexturePacker这款软件来打图集
-将TexturePacker打的图集转成Unity的Sprites ，注意当前只支持 Sparrow/Starling 格式


#SpritesToPng
将Sprite图集导出成一张一张的png
使用方法：在unity中选中图集，再点击Tools/SpritesToPng，最后导出的位置在Unity工程文件夹下面(Assets上一级)

# PlayerPrefsX
支持一些复杂点的数据格式，比如数组



# InputUtil
判断touch是否在UGUI上
'bool InputUtil.CheckMouseOnUI()'


# Matrix2D
2D 矩阵



# AudioManager
对声音进行简单的封装，声音路径是指Resources下面。
建议在场景切换的时候，调用一次AudioManager.Instance.StopAll();



# ResManger
主要是加载和管理图片，精灵，图集，配合PngProcess使用实现加载加密图片



# DynamicAsset
让SpriteRenderer和Image显示StreamingAssets中的图片，以及替换Material中的贴图

## SpriteRenderer和Image使用方法：
1. 在SpriteRenderer或Image的对象上添加DynamicAsset脚本
2. 填写图片路径，需要.png或.jpg的后缀。  (StreamingAssets的图片，路径不包括StreamingAssets)
3. 如果是图集，需要填写图集配置.xml的路径，例如 sprites.xml ， 其次还需要填frameName参数
4. 点击refresh按钮刷新

## 替换Material贴图使用方法
1. 在任意GameObject上面添加DynamicAsset脚本
2. 选中replaceMatTexture
3. 设置material，nameId
4. restoreOnDisable参数表示在diable时会将材质贴图设置为prevTex，主要用在Spine的材质上
5. 点击refresh按钮刷新

## AutoLoad和初始化
1. 如果设置为选中，则游戏运行时，会自动替换图片
2. 如果没选中，则需要在场景加载后，调用初始化DynamicAsset的方法，（调用一次即可）：
初始化DynamicAsset,主要用于加载和显示所有的DynamicAsset图片，可以在新场景加载完后进行初始化，然后再移除Loading.
'public static void Init(DynamicAsset[] dynamicAssets , System.Action onInited, System.Action<float> onProgress = null)'

--建议将大量需要替换显示的对象时，使用非AutoLoad方式，以免刚开始出现图片闪现情况。

## AutoDisposeAsset
在该对象Destroy时，会自动删除加载的资源，但是先确保其他地方没有使用此Asset，不然也会一起销毁






# 图片加密 (处理StreamingAssets下面的图片)
只是简单地把图片的二进制数据倒置。
在Unity菜单栏 Tools/Encrypt.... 有三项，一个是处理整个StreamingAssets里面的图片，一个是处理一个文件夹中的图片
一个是处理一张图片。

处理过的图片再处理一次会还原。






# BuildPostProcess 
发布后期处理，发布xcode和android项目后，不需要手动替换资源。
当ios和android项目发布后，会自动替换proj.ios_mac中的Class/Native和Data文件夹。 Android项目会替换assets资源文件夹。