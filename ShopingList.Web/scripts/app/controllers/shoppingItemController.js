shopingListApp.controller('shoppingItemController', ['$scope', '$http', function ($scope, $http) {
    $scope.productList = [];
    $scope.categoryList = [];
    $scope.shoppingList = [];
    $scope.checkedItems = 0;
    $scope.listSortReverse = false;
    $scope.listSortType = 'Product.Name';
    $scope.itemSortReverse = false;
    $scope.itemSortType = 'Name';

    $scope.categoryList = categoryListGlobal;

    $http.get('/ShoppingItems/GetShoppingItemsAsync')
      .success(function (data) {
          $scope.shoppingList = data;
          initShoppingItemList($scope.shoppingList);
      }).error(function (response) {
          console.log(response.message);
      });

    $http.get('/Product/GetAllProductsAsync')
        .success(function (data) {
            $scope.productList = data;
        }).error(function (response) {
            console.log(response.message);
        });

    $scope.addToList = function (product) {
        $http.post('/ShoppingItems/AddShoppingItemAsync', product)
            .success(function (data) {
                var shoppingItemId = data.replace('"', '').replace('"', '');
                onSuccessAddItem(shoppingItemId, product);
            }).error(function (response) {
                console.log(response.message);
            });
    }

    $scope.removeFromList = function (shoppingItem) {
        $http.post('/ShoppingItems/RemoveShoppingItemAsync', { shoppingItemId: shoppingItem.ShopingItemId })
            .success(function () {
                removeItem(shoppingItem.ShopingItemId, $scope.shoppingList);
            }).error(function (response) {
                console.log(response.message);
            });
    }

    $scope.toggleItem = function (shoppingItem) {
        shoppingItem.IsChecked = !shoppingItem.IsChecked;
        if (shoppingItem.IsChecked) {
            $scope.checkedItems++;
        } else {
            $scope.checkedItems--;
        }
    }

    $scope.clearAll = function () {
        $http.post('/ShoppingItems/ClearAllShoppingItemsAsync')
           .success(function () {
               $scope.shoppingList = [];
                $scope.checkedItems = 0;
            }).error(function (response) {
               console.log(response.message);
           });
    }

    $scope.checkOut = function () {
        var checkedItems = $.grep($scope.shoppingList, function (val) {
            return val.IsChecked;
        });

        $http.post('/ShoppingItems/CheckOutShoppingItemsAsync', checkedItems)
            .success(function () {
                $.each(checkedItems, function (i, item) {
                    for (var j = 0; j < $scope.shoppingList.length; j++) {
                        if ($scope.shoppingList[j].ShopingItemId === item.ShopingItemId) {
                            $scope.shoppingList.splice(j, 1);
                            break;
                        }
                    }
                });
            }).error(function (response) {
                console.log(response.message);
            });
    }

    function onSuccessAddItem(shoppingItemId, product) {
        var newShoppingItem = { ShopingItemId: shoppingItemId, Product: product };
        $scope.shoppingList.splice(0, 0, newShoppingItem);
    }

    function removeItem(shoppingItemId, list) {
        var index = -1;
        $.each(list, function (indx, val) {
            if (val.ShopingItemId === shoppingItemId) {
                index = indx;
            }
        });

        if (index >= 0)
            list.splice(index, 1);
    }

    function initShoppingItemList(shoppingList) {
        $.each(shoppingList, function (indx, val) {
            val.IsChecked = false;
        });
    }
}]);