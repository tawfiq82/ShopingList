shopingListApp.controller('categoryController', ['$scope', '$http', function ($scope, $http) {
    $scope.categoryList = [];
    $scope.isAdding = false;
    $scope.isDeleting = false;
    $scope.isDuplicateCategory = false;

    $http.get('/Category/GetCategories')
        .success(function (data) {
            $scope.categoryList = data;
        }).error(function (response) {
            console.log(response.message);
        });

    $scope.addCategory = function (form) {
        $scope.isAdding = true;
        if (form && form.$invalid)
            return;

        if (isDuplicateCategory($scope.category.Name)) {
            $scope.isDuplicateCategory = true;
            return;
        }

        $http.post('/Category/AddCategory', $scope.category)
            .success(function (data) {
                data = data.replace('"', '').replace('"', '');
                onSuccessAddCategory(data);
            }).error(function (response) {
                console.log(response.message);
            }).finally(function () {
                $scope.isAdding = false;
            });
    }

    $scope.deleteCategory = function (categoryId) {
        $scope.isDeleting = true;
        $http.post('/Category/DeleteCategory', { "categoryId": categoryId })
                    .success(function (data) {
                        onSuccessDeleteCategory(categoryId);
                    }).error(function (response) {
                        console.log(response.message);
                    }).finally(function () {
                        $scope.isDeleting = false;
                    });
    }

    $scope.checkDuplicateCategory = function (categoryName) {
        $scope.isDuplicateCategory = isDuplicateCategory(categoryName);
    }

    function onSuccessAddCategory(categoryId) {
        $scope.category.CategoryId = categoryId;
        var newCategory = $.extend(true, {}, $scope.category);
        $scope.categoryList.splice(0, 0, newCategory);
        $scope.category.Name = '';
        $scope.category.CategoryId = '';
    }

    function onSuccessDeleteCategory(categoryId) {
        var index = -1;
        $.each($scope.categoryList, function (indx, val) {
            if (val.CategoryId === categoryId) {
                index = indx;
            }
        });

        if (index >= 0)
            $scope.categoryList.splice(index, 1);
    }

    function isDuplicateCategory(categoryName) {
        if (categoryName == null || categoryName === '')
            return false;

        var duplicateCategory = $.grep($scope.categoryList, function (val) {
            return val.Name.toUpperCase() === categoryName.toUpperCase();
        });

        if (duplicateCategory != null && duplicateCategory.length > 0)
            return true;
        return false;
    }

}]);