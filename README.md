# ShopBridgeAPI

Platform independent Web API calls to insert, update, delete and list products.

Solution can be downloaded or cloned and run directly. The interface will show swagger interface to run REST APIs.

List of APIs:

1. Add Product.
2. Update Product.
3. Delete Product.
4. List Products.
5. Get Product By ID.
6. Upload Image for a Product.
7. Book Product.
8. Add Product Stock.
9. Make Product Out of Stock.

### List Products

Request URL : /api/Products
Request Body : None
Response Value Schema :
[
  {
    "productID": 0,
    "productName": "string",
    "productDescription": "string",
    "productCategory": "string",
    "productPrice": 0,
    "productQuantity": 0,
    "productImage": "string",
    "inStock": true,
    "isDeleted": false
  }
]

### Add Product

#Request URL : /api/Products
#Request Body : JSON string
[
  {
    "productName": "string",
    "productDescription": "string",
    "productCategory": "string",
    "productPrice": 0,
    "productQuantity": 0,
    "inStock": true,
  }
]
#Response Value Schema :
[
  {
    "productID": 0,
    "productName": "string",
    "productDescription": "string",
    "productCategory": "string",
    "productPrice": 0,
    "productQuantity": 0,
    "productImage": "string",
    "inStock": true,
    "isDeleted": false
  }
]

### Get Product By ID

#Request URL : /api/Products/{id}
#Request Body : None
#Response Value Schema :
[
  {
    "productID": 0,
    "productName": "string",
    "productDescription": "string",
    "productCategory": "string",
    "productPrice": 0,
    "productQuantity": 0,
    "productImage": "string",
    "inStock": true,
    "isDeleted": false
  }
]

### Update Product

#Request URL : /api/Products/{id}
#Request Body : JSON string
[
  {
    "productName": "string",
    "productDescription": "string",
    "productCategory": "string",
    "productPrice": 0,
    "productQuantity": 0,
    "inStock": true,
  }
]

### Upload Image for a Product

#Request URL : /api/Products/uploadimage/{id}
#Request Body : None
#Response : Image uploaded successfully.

### Add Product Stock

#Request URL : /api/Products/addstock/{id}/{quantity}
#Request Body : None
#Response : Stock updated successfully.

### Book Product

#Request URL : /api/Products/booking/{id}/{quantity}
#Request Body : None
#Response : Product booked successfully.

### Make Product Out of Stock

#Request URL : /api/Products/outofstock/{id}
#Request Body : None

![List of APIs](https://user-images.githubusercontent.com/87942579/127224377-8fceb1cc-6dee-4e3a-b544-57053b34b31e.PNG)

![AddProduct](https://user-images.githubusercontent.com/87942579/127224229-61fb8655-3d3b-4c83-83fb-d0d3a6c249b2.PNG)

![AddStock](https://user-images.githubusercontent.com/87942579/127224302-3b1204a0-3894-434d-a023-2495ac595a6d.PNG)

![BookProduct](https://user-images.githubusercontent.com/87942579/127224319-ff516f7a-1792-40bf-96e7-4d29354a475a.PNG)

![GetProductByID](https://user-images.githubusercontent.com/87942579/127224342-3b9fb564-e288-4365-94fb-fb9c263c29cc.PNG)

![GetProducts](https://user-images.githubusercontent.com/87942579/127224361-6a56edfa-d7e5-4d36-88b1-668de3ae3d20.PNG)

![UpdateProduct](https://user-images.githubusercontent.com/87942579/127224427-e320a13e-6ecc-489e-8c32-e77dd9095557.PNG)

![UploadImage](https://user-images.githubusercontent.com/87942579/127224445-35200b47-85d2-499f-b94a-64f3b2684d9f.PNG)

