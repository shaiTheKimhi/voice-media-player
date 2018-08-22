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



#compares two lists
#returns two lists
#first list is additions
#second list is deletions
def get_changes(list1, list2):
    return [i for i in list1 if i not in list2],[i for i in list2 if i not in list1]
