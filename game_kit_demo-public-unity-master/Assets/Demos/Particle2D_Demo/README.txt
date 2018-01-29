#注意：
pex,plist配置需要在后面加.txt后缀，才能拖到Effect Config上面。


#使用步骤：
1.通过Unity菜单上的Particle2D->Particle2D UGUI/Particle2D System创建，一个是用于UI，一个是普通的。
如果是Particle2D UGUI，需要将创建好的粒子拖到Canvas下面才会显示。创建好对象后就可以在属性面板设置参数。


2. 调试粒子参数 Config Values


3.替换粒子贴图
将新的贴图拖到 Config Values / Texture处



4.替换材质
如果你有特殊的混合模式或者其他特效，创建你自己的材质，拖到属性面板的Material上面。或者选择已有的Material来替换


5. 导入pex和plist粒子 (主要由第三方工具制作的粒子)
-首先将pex和plist文件后面加上.txt后缀，unity才会把它当作文本文件处理。
-其次将pex和plist文件拖到属性面板的Effect Config处，然后点击下面的Load From Config按钮。
-将需要的粒子贴图拖到 Config Values / Texture处



6. API
void Play();
void Stop(bool clear = false);