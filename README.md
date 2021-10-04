# warehouse-app

You need to create web app that provides API of distributed warehouses.
Main functions:
1) return list of all SKUs
2) return info about one SKU
how much items left in each warehouse
how much items are reserved
how much items are planned to be delivered soon
3) reserve SKU
reservation should have expiration time
reservation could be done within few warehouse, but this should be done only in case no one warehouse has required amount of SKUs
4) remove reservation of SKU
not abstract, but one of the previous
5) SKU is sold
not only reserved SKUs` can be sold
sold SKUs` are removed from warehouse
invoice should be saved
invoice number is returned
6) return list of all warehouses
7) return info of one warehouse
how much goods are stored
how much goods are reserved
how much free space available
8) add goods to warehouse
9) return list of all invoices
10) return info about one invoice
11) return all goods with invoice
goods can be returned to different warehouse

You need to use Asp.Net Core.
Place solution to your Github account. Commit changes after each step is completed.
