# E-Commerce RESTful API using ASP.Net and SQL

Welcome to the documentation for our E-Commerce RESTful API, built using ASP.Net and SQL Server. This API provides endpoints for managing roles, accounts, categories, products, and orders within the E-Commerce system. Below is an overview of the available endpoints and their functionalities:

## 1. Roles Controller

- **GetAllRoles:** View all available roles. (Accessible only to administrators)
  `Endpoint: /api/Roles/GetAllRoles`

- **CreateRole:** Create a new role for the roles table. (Accessible only to administrators)
  `Endpoint: /api/Roles/CreateRole`

- **AddRoleToUser:** Add a new role for a specific user based on their name. (Accessible only to administrators)
  `Endpoint: /api/Roles/AddRoleToUser`

## 2. Account Controller

- **Register:** Register a new user and assign a role in the DTO model.
  `Endpoint: /api/Account/Register`

- **Login:** Log in and retrieve an authentication token.
  `Endpoint: /api/Account/Login`

## 3. Categories Controller

- `Create`: Create a new category. (Accessible only to administrators)
  `Endpoint: /api/Categories/Create`

- `GetAll`: Retrieve all categories.
  `Endpoint: /api/Categories/GetAll`

- `GetOne`: Retrieve a specific category by its ID.
  `Endpoint: /api/Categories/GetOne/{ID}`

- `Update`: Update the name of a category by its ID. (Accessible only to administrators)
  `Endpoint: /api/Categories/Update/{ID}`

- `Delete`: Delete a category by its ID. (Accessible only to administrators)
  `Endpoint: /api/Categories/Delete/{ID}`

## 4. Products Controller

- **Create:** Create a new product and associate it with a vendor ID. (Accessible only to vendors)
  `Endpoint: /api/Products/Create`

- **GetAll:** Retrieve all products. Can filter by category name using a query string.
  `Endpoint: /api/Products/GetAll?categoryName=clothes`

- **GetOne:** Retrieve a specific product by its ID.
  `Endpoint: /api/Products/GetOne/{ID}`

- **Update:** Update product details. Vendors can update their own products, and administrators can update any product.
  `Endpoint: /api/Products/Update/{ID}`

- **Delete:** Delete a product by its ID. Vendors can delete their own products, and administrators can delete any product.
  `Endpoint: /api/Products/Delete/{ID}`

## 5. Orders Controller

- **Create:** Create a new order and link it to a user ID. (Accessible only to users)
  `Endpoint: /api/Orders/Create`

- **GetAll:** Retrieve all orders. Users can view their own orders, vendors can see orders for their products only, and administrators have access to all orders.
  `Endpoint: /api/Orders/GetAll`

- **GetOne:** Retrieve a specific order by its ID. Users can access their own orders, vendors can view orders for their products, and administrators can view any order.
  `Endpoint: /api/Orders/GetOne/{ID}`

- **Update:** Update the status of an order. (Accessible only to administrators)
  `Endpoint: /api/Orders/Update/{ID}`

- **Delete:** Delete an order by its ID. (Accessible only to administrators)
  `Endpoint: /api/Orders/Delete/{ID}`

Database Backup
To facilitate testing, you can use the provided database backup file ECommerceAPI.bak.

Please refer to the specific endpoints for detailed usage instructions and access restrictions. If you encounter any issues or have questions, feel free to reach out to us. Happy coding!
