shopingListApp.controller('productController', ['$scope', '$http', function ($scope, $http) {
    $scope.productList = [];
    $scope.categoryList = [];
    $scope.isAdding = false;
    $scope.isDeleting = false;

    $scope.categoryList = categoryListGlobal;

    $http.get('/Product/GetAllProductsAsync')
        .success(function (data) {
            $scope.productList = data;
        }).error(function (response) {
            alertBox.showErrorMessage(response.message);
            console.log(response.message);
        });

    $scope.addProduct = function (form) {
        $scope.isAdding = true;
        if (form && form.$invalid)
            return;

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
}]);