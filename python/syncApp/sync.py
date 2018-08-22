import sys, os
import time
from threading import Thread

current  = os.getcwd()
sourceDir = "\songs"
if(len(sys.argv) > 1):
    sourceDir = sys.argv[1]
listDir = os.listdir(current + sourceDir)

def sync_changes():
    global listDir
    while(True):
        time.sleep(2)
        tempDir = os.listdir(current + sourceDir)
        changes = get_changes(tempDir, listDir)
        #reorganizing the array into listDir and changes to additions and deletions
        listDir = tempDir
        additions = changes[0]
        deletions =  changes[1]
        update_changes(additions,deletions)



#compares two lists
#returns two lists
#first list is additions
#second list is deletions
def get_changes(list1, list2):
    return [i for i in list1 if i not in list2],[i for i in list2 if i not in list1]

#responsible for saving directory changes
def update_changes(additions, deletions):
    for file in deletions:
        delete_from_server(file)
    for file in additions:
        save_to_server(file)

#saves the given file to remote server
def save_to_server(file):
    no = None
#deletes the given file from the remote server
def delete_from_server(file):
    no = None


