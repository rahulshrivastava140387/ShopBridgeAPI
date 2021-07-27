# ShopBridgeAPI

Platform independent Web API calls to insert, update, delete and list products.

Features:

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

Request URL : /api/Products
Request Body : JSON string
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

### Get Product By ID

Request URL : /api/Products/{id}
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

### Update Product

Request URL : /api/Products/{id}
Request Body : JSON string
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

Request URL : /api/Products/uploadimage/{id}
Request Body : None
Response : Image uploaded successfully.

### Add Product Stock

Request URL : /api/Products/addstock/{id}/{quantity}
Request Body : None
Response : Stock updated successfully.

### Book Product

Request URL : /api/Products/booking/{id}/{quantity}
Request Body : None
Response : Product booked successfully.

### Make Product Out of Stock

Request URL : /api/Products/outofstock/{id}
Request Body : None