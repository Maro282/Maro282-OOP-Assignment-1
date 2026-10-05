### 1- Global variables
the program saves customers, products, orders data in global arrays and global variables . It will be Accessible from anywhere in the program 

### 2- Ownership
you can't determine who is the owner of data and who should modify and manipulate this part of data 

### 3- Program logic 
program logic literally in the same place there is no responsibility separation , and data is separated from business logic  

### 4- Fixed arrays
arrays is with fixed sizes and that will be a vulnerability if we want to increase the scale of the project

### 5- Related data is separated 
like customer represented in around 3 or four arrays . so to display data for one customer we should print from 4 arrays 

### 6- Order depends on array indexes
Orders do not directly contain customer or product objects. they depend on indexes and  store indexes such as orderCustomerIndexes and lineProductIndexes.

### 7- Order line depends on two parallel arrays
line information stored in two separated arrays lineProductIndexes and lineQuantities
the two arrays indexes must be synced and if by accident stores wrong indexes for one line we are cooked  