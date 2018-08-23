Server Requests specification
=============================

## Client to Server Requests:  
1.  *Create Directory* -  
    code : '0'  
    description : creates a new directory at the storage  
    format : '0Directory'  
2. *Update Directory* -   
    code : '1'  
    description : updates the directory files to the data sent  
    format : '1JSON({'deletions':[...] , 'additions':[file1, ...], file1:})'
3. *Get Directory* -  
    code : '2'  
    description : requests files of specific directory
    format : '2Directory'
    response : 'JSON({files:[file1, ...], file1:...})' // add referance to server to client response of this type

