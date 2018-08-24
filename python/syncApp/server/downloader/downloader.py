import pytube
import urllib.request, urllib.parse


#gets search request and returns link to first youtube result
def get_video_link(query):
    res = urllib.request.urlopen("https://youtube.com/search?q="+query)
    html = res.read().decode('utf-8')
    parts = html.split("<a")
