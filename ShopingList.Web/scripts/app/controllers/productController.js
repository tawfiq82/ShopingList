shopingListApp.controller('productController', ['$scope', '$http', function ($scope, $http) {
    $scope.productList = [];
    $scope.categoryList = [];
    $scope.isAdding = false;
    $scope.isDeleting = false;
    $scope.isDuplicateProduct = false;
    $scope.categoryList = categoryListGlobal;
    $scope.isUpdating = false;

    $http.get('/Product/GetAllProductsAsync')
        .success(function (data) {
            $scope.productList = data;
            $.each($scope.productList, function (indx, val) {
                val.IsUpdating = false;
                val.HideControls = false;
            });
        }).error(function (response) {
            alertBox.showErrorMessage(response.message);
            console.log(response.message);
        });

    $scope.addProduct = function (form) {
        $scope.isAdding = true;
        if (form && form.$invalid)
            return;

        if (isDuplicateProduct($scope.product)) {
            $scope.isDuplicateProduct = true;
            return;
        }

        alertBox.hide();
        $http.post('/Product/AddProductAsync', $scope.product)
            .success(function (data) {
                data = data.replace('"', '').replace('"', '');
                onSuccessAddProduct(data);
            }).error(function (response) {
                alertBox.showErrorMessage(response.message);
                console.log(response.message);
            }).finally(function () {
                $scope.isAdding = false;
            });
    }

    $scope.deleteProduct = function (product) {
        alertBox.hide();
        $scope.isDeleting = true;
        $http.post('/Product/DeleteProductAsync', product)
                    .success(function (data) {
                        onSuccessDeleteProduct(product.ProductId);
                    }).error(function (response) {
                        alertBox.showErrorMessage(response.message);
                        console.log(response.message);
                    }).finally(function () {
                        $scope.isDeleting = false;
                    });
    }

    $scope.checkDuplicate = function (product) {
        $scope.isDuplicateProduct = isDuplicateProduct(product);
    }

    function onSuccessAddProduct(productId) {
        $scope.product.ProductId = productId;
        var newProduct = $.extend(true, {}, $scope.product);
        setCategoryName(newProduct);
        $scope.productList.splice(0, 0, newProduct);
        $scope.product.Name = '';
        $scope.product.CategoryId = '';
        $scope.product.ProductId = '';
        alertBox.showSuccessMessage("Successfully added the product.");
    }

    function onSuccessDeleteProduct(productId) {
        var index = -1;
        $.each($scope.productList, function (indx, val) {
            if (val.ProductId === productId) {
                index = indx;
            }
        });

        if (index >= 0)
            $scope.productList.splice(index, 1);

        alertBox.showSuccessMessage("Successfully deleted the product.");
    }

    function setCategoryName(product) {
        var category = $.grep($scope.categoryList, function (val) {
            return val.CategoryId === product.Category.CategoryId;
        });
        if (category != null && category.length > 0)
            product.Category = category[0];
    }

    function isDuplicateProduct(product) {
        if (product.Name === '')return false;
        var duplicateProduct = $.grep($scope.productList, function (val) {
            return val.Name.toUpperCase() === product.Name.toUpperCase()
             && (product.ProductId == undefined || val.ProductId !== product.ProductId);
        });

        if (duplicateProduct != null && duplicateProduct.length > 0)
            return true;

        return false;
    }

    $scope.editProduct = function (product) {
        $.each($scope.productList, function (indx, val) {
            if (val.ProductId === product.ProductId) {
                val.IsUpdating = true;
            } else {
                val.HideControls = true;
            }
        });
        $scope.isUpdating = true;
    }

    $scope.updateProduct = function (form, product) {

        var invalid = false;

        if (product.Name == undefined || product.Name === '') {
            form.updateName.$error.required = true;
            invalid = true;
        } else {
            form.updateName.$error.required = false;
        }

        if (product.Category.CategoryId == undefined) {
            form.updateCategory.$error.required = true;
            invalid = true;
        } else {
            form.updateCategory.$error.required = false;
        }

        if (invalid || $scope.isDuplicateProduct)
            return;

        alertBox.hide();
        $http.post('/Product/UpdateProductAsync', product)
            .success(function () {
                setCategoryName(product);
                alertBox.showSuccessMessage("Successfully updated the user.");
            }).error(function (response) {
                alertBox.showErrorMessage(response.message);
                console.log(response.message);
            }).finally(function () {
                $.each($scope.productList, function (indx, val) {
                    if (val.ProductId === product.ProductId) {
                        val.IsUpdating = false;
                    } else {
                        val.HideControls = false;
                    }
                });
                $scope.isUpdating = false;
            });
    }

    $scope.cancelUpdate = function (product) {
        $.each($scope.productList, function (indx, val) {
            if (val.ProductId === product.ProductId) {
                val.IsUpdating = false;
            } else {
                val.HideControls = false;
            }
        });
        $scope.isUpdating = false;
    }
}]);