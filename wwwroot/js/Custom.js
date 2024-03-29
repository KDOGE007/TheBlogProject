﻿let index = 0;

function AddTag() {
    // Reference to TagEntry input element
    var tagEntry = document.getElementById("TagEntry");

    // Use search function to detect errors
    let searchResult = search(tagEntry.value);
    if (searchResult != null) {
        // Trigger Sweet Alert for error condition (searchResult variable)
        swalWithDarkButton.fire({
            html: `<span class='font-weight-bolder'>${searchResult.toUpperCase()}</span>`
        });

    } else {
        // Create new select option
        let newOption = new Option(tagEntry.value, tagEntry.value);
        document.getElementById("TagList").options[index++] = newOption;
    }

    // Clear TagEntry control
    tagEntry.value = "";
    return true;
}

function DeleteTag() {
    let tagCount = 1;
    const tagList = document.getElementById("TagList");
    if (!tagList) return false;

    if (tagList.selectedIndex < 0) {
        swalWithDarkButton.fire({
            html: "<span class='font-weight-bold'>CHOOSE A TAG BEFORE DELETEING</span>"
        })
        return true;
    }

    while (tagCount > 0) {
            if (tagList.selectedIndex  >= 0) {
                document.getElementById("TagList").options[tagList.selectedIndex] = null;
                --tagCount;
            }
            else
                tagCount = 0;
        index--;
    }
}

$("form").on("submit", function () {
    $("#TagList option").prop("selected", "selected");
})

//Look for the tagValues to see if it has data
if (tagValues != "") {
    let tagArray = tagValues.split(",");
    for (let loop = 0; loop < tagArray.length; loop++) {
        //Load up or Replace the options that we have
        ReplaceTag(tagArray[loop], loop);
        index++;
    }
}

function ReplaceTag(tag, index) {
    let newOption = new Option(tag, tag);
    document.getElementById("TagList").options[index] = newOption;
}

//The Search funtion will detect either an empty or a duplicate Tag
//and return an error string if an error is detected
function search(str) {
    if (str == "") {
        return "Empty tags are not permitted";
    }

    let tagsEl = document.getElementById("TagList");
    if (tagsEl) {
        let options = tagsEl.options;
        for (let index = 0; index < options.length; index++) {
            if (options[index].value == str) {
                return `The Tag #${str} was detected as a duplicate and not permitted`;
            }
        }
    }
}

const swalWithDarkButton = Swal.mixin({
    customClass: {
        confirmButton: "btn btn-danger btn-sm btn-outline-dark"
    },
    imageUrl: "/img/oops.png",
    timer: 3000,
    buttonsStyling: false
})