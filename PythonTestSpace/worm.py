import sys
import urllib
import urllib2
from bs4 import BeautifulSoup
import re
from pymongo import MongoClient

class wm(object):
    def run(_self):
        client = MongoClient('172.16.10.88',27017)
        dtm = client.amac
        tcl = dtm.amacs

        url = 'www.baidu.com'
        hc = urllib.urlopen(url).read()
        print(hc)