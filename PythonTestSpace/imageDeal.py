import matplotlib.image as mpimg
from numpy.core import shape
from numpy.lib import shape # mpimg 用于读取图片
import numpy as np
import matplotlib.pyplot as plt # plt 用于显示图片


class myimgDeal(object):

    def showImg(self):        
        fileName = 'C:\\Users\\wyb\\Desktop\\123.png'
        lena = mpimg.imread(fileName) # 读取和代码处于同一目录下的 lena.png
        # 此时 lena 就已经是一个 np.array 了，可以对它进行任意处理
        lena.shape#(512, 512, 3) 
        plt.imshow(lena) # 显示图片
        plt.axis('off') # 不显示坐标轴
        plt.show()
