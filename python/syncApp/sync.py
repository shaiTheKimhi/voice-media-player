import os
import time


def sync_changes():
    global listDir
    while(True):
        time.sleep(2)
        tempDir = os.listdir(os.getcwd()+ sourceDir)
        changes = get_changes(tempDir, listDir)
        #reorganizing the array into listDir and changes to additions and deletions
        listDir = tempDir
        additions = changes[0]
        deletions =  changes[1]
        update_changes(additions,deletions)
