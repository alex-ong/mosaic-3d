from PIL import Image
import os

class PixelCounter(object):
  ''' loop through each pixel and average rgb '''
  def __init__(self, imageName):
      self.pic = Image.open(imageName)
      # load image data
      self.imgData = self.pic.load()
  #change this to whatever you want!
  def averagePixels(self):
      r, g, b = 0, 0, 0
      count = 0
      for x in xrange(self.pic.size[0]):
          for y in xrange(self.pic.size[1]):
              tempr,tempg,tempb = self.imgData[x,y]
              r += tempr
              g += tempg
              b += tempb
              count += 1
      # calculate averages
      return (r/count), (g/count), (b/count)

if __name__ == '__main__':
  # assumes you have a test.jpg in the working directory! 
  for name in os.listdir('CacheFiles'):    
    pc = PixelCounter('CacheFiles/'+name)  
    print name+','+str(pc.averagePixels())    
