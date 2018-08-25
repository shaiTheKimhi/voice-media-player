import pytube
import urllib.request, urllib.parse
from pytube import YouTube

#gets search request and returns link to first youtube result
def get_video_link(query):
    
    res = urllib.request.urlopen("https://youtube.com/search?q="+query)
    html = res.read().decode('utf-8')
    # lnk = extract_link(html)
    # print(lnk)
    return extract_link(html)

def stringify(query):
    query = query.replace(" ", "%20")
    #TODO : handle all special characters
    return query
    
def extract_link(html):
    index = html.index('class=" yt-uix-sessionlink      spf-link "')
    sub = html[index - 13:index - 2]
    return sub

def download_link(lnk):
    YouTube("youtu.be/" + lnk).streams.first().download()
