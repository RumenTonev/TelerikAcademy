//two methods for easier working with array in localstorage

Storage.prototype.getArray = function (arrayName) {
    var thisArray = [];
    var fetchArrayObject = this.getItem(arrayName);   
    if (fetchArrayObject === "") { return thisArray; }
    else if (typeof fetchArrayObject !== 'undefined') {

        if (fetchArrayObject !== null) { thisArray = JSON.parse(fetchArrayObject); }
        else {
            this.setItem(arrayName, thisArray);
        }
    }

    return thisArray;
}

Storage.prototype.pushArrayItem = function (arrayName, arrayItem) {
    var existingArray = this.getArray(arrayName);
    existingArray.push(arrayItem);
    this.setItem(arrayName, JSON.stringify(existingArray));
}