#注意:

-如果要自己写shader，如果报错找不到FiltersCG.cginc，则需要改成绝对路径，如：
#include "Assets/GameKit/Filter2D/Shader/FiltersCG.cginc"

-如果要使用外发光，可能Sprite在导入的时候需要设置Extrude Edges ，最好大一些，比如10像素