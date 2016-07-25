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
            console.log(response.message);
        });

    $scope.addProduct = function (form) {
        $scope.isAdding = true;
        if (form && form.$invalid)
            return;

        $http.post('/Product/AddProduct', $scope.product)
            .success(function (data) {
                data = data.replace('"', '').replace('"', '');
                onSuccessAddProduct(data);
            }).error(function (response) {
                console.log(response.message);
            }).finally(function () {
                $scope.isAdding = false;
            });
    }

    $scope.deleteProduct = function (productId) {
        $scope.isDeleting = true;
        $http.post('/Product/DeleteProduct', { "productId": productId })
                    .success(function (data) {
                        onSuccessDeleteProduct(productId);
                    }).error(function (response) {
                        console.log(response.message);
                    }).finally(function () {
                        $scope.isDeleting = false;
                    });
    }

    function onSuccessAddProduct(productId) {
        $scope.product.ProductId = productId;
        var newProduct = $.extend(true, {}, $scope.product);
        $scope.productList.splice(0, 0, newProduct);
        $scope.product.Name = '';
        $scope.product.CategoryId = '';
        $scope.product.ProductId = '';
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
    }
}]);