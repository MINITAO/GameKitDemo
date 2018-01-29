# Bone2D
是一款将DragonBones和Spine动画转换成Unity自带的动画插件。



# 注意事项
 - 不支持碎图，所以导出时要导出图集
 - 混合模式支持不好，建议动画中不要使用
 - Unity动画和DragonBones、Spine动画最大的区别在于第一帧。Unity动画第一帧的值等于最近的一帧的值，比如第一帧没有关键帧
第三帧才有关键帧，则unity动画中第一帧的值就等于第三帧的值。 而在DragonBones和Spine中，第一帧如果没有关键帧，第三帧才有的话，
则第一帧的值等于Pose的值
   所以建议第一个动画的第一帧最好K一个关键帧，这样就可以和Unity动画保持值一致
 


# DragonBones不支持的有:
 - IK约束支持不好，建议动画中不要用
 - 动画中旋转超过360度支持不好，建议旋转还是使用原始方式，比如旋转一圈可以隔90度K一帧
 - 帧事件支持不完整，建议生成动画后在unity里面设置帧事件
 - 曲线编辑器多段贝塞尔曲线不支持，生成动画后只会取前后两个点，中间的编辑点不会取

# Spine不支持的有:
 - IK约束支持不好, Transform和Path约束不支持，建议在动画中不要使用
 - 扭曲(Shear)不支持




# Bone2D使用方法
 1. 将DragonBones或Spine导出的贴图，动画等配置文件放到一个文件夹中
 2. 将文件夹选中，或者选中文件夹中的文件
 3. 点右键或菜单栏中的Bones->中的选项，就会自动生成动画
 4. 如果选择生成的是UI动画，则需要把生成后的对象拖到UGUI的Canvas下面
 5. 如果你要准确控制参数，可以在选中文件或文件夹后，选择DragonBones Panel或Spine Panel
 6. 如果图集有'Premultiply Alpha'，则需要勾选Armature组件上的Pre Multiply Alpha，否则不勾选
 7. 反转可以设置Armature组件上的FlipX或FlipY，也可以设置localScale.x为-1
 8. 在DragonBones中，如果导出的时候有贴图缩放，默认骨骼也会缩放，解决办法是先导出1:1的骨骼数据，然后再导出缩放后的贴图和贴图数据，
将1:1的骨骼数据和缩放后的贴图数据结合，在生成Bone2D时，选择DragonBones Panel，设置TextureScale参数为你导出缩放时的贴图的缩放值。
 9. 初始动画设置，在Armature组件的'Current Animation'，后期你就可以用Animator控制动画
 10.DragonBones/Spine Panel中的'Generic Anim'参数很重要，如果勾选，则生成出来的动画Slots会单独列出来，这种方式生成后的动画文件
UI和普通动画是通用的。如果不勾选，则Slot会添加到对应的骨骼下面，但是生成出来的动画UI动画和普通动画就需要两套。
 11. 点击unity菜单栏->Bone2D->'Update Sprites'按钮，会从材质中的MainTexture中重新读取Sprite。



# API
如果要调用API，需要using Bone2D
void SetToPose()
void ResetSlotZOrder()
void ForceSortAll()
Renderer GetAttachmentByName( string attachmentName)
MaskableGraphic GetUIAttachmentByName( string attachmentName)
//change相关的方法全是和换图片相关的API
Change.....()






#AnimatorController NO Transition
选中AnimatorController，点击此项，会将AnimatorController中的节点的过渡时间全部设置为0。
可以防止动画过渡时间过长引起的动画bug





# DragonBonesToSpineData
是一款将DragonBones数据转成Spine数据的软件,包含Mac和Win版本。
虽然DragonBones和Spine功能大致相同，但也有些不一样

##转换方法:
将DragonBones导出的数据和贴图放入一个文件夹中，然后打开这个转换软件，将这个文件夹拖入软件中

不支持的有：(主要是一些DragonBones有而Spine没有功能)
 - DragonBones的骨骼嵌套不支持
 - DragonBones的旋转多圈(超360度旋转)
 - DragonBones的曲线编辑器多段贝塞尔曲线
 - 事件转换可能有问题，DragonBones的事件要复杂得多