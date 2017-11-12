import urllib
import sys
import re
from pymongo import MongoClient
import scrapy
 
class DmozSpider(scrapy.Spider):
    name = "dmoz"
    allowed_domains = ["dmoz.org"]
    start_urls = [
        "http://www.dmoz.org/Computers/Programming/Languages/Python/Books/",
        "http://www.dmoz.org/Computers/Programming/Languages/Python/Resources/"
    ]
 
    def parse(self, response):
        filename = response.url.split("/")[-2]
        with open(filename, 'wb') as f:
            f.write(response.body)

class wm(object):
    def run():
        client = MongoClient('172.16.60.168',27017)
        db_tms = client.amac
        tag_cls = db_tms.amacs

        url = 'http://www.baidu.com'
        pager = urllib.request(url)

        print(pager)
