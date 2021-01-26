// Write your JavaScript code.

showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#queryform-modal .modal-body").html(res);
            $("#queryform-modal .modal-title").html(title);
            $("#queryform-modal").modal('show');
        }
    })
}

//ResolveQueryPost = form => {
//    try {
//        $.ajax({
//            type: "POST",
//            url: form.action,
//            data: new FormData(form),
//            contentType: false,
//            processData: false,
//            success: function (res) {
//                //check if model is valid.... validate form input fields
//               //// if (res.isValid) {
//                  //  $("#view-all").html(res.html);
//                   // $("#queryform-modal .modal-body").html('');
//                   // $("#queryform-modal .modal-title").html('');
//                    $("#queryform-modal").modal('hide');
                    
//                   // $.notify('Submitted Successfully', { globalPosition: 'top center', className: 'success' })
//                ////}
//                ////else
//                ////    $("#form-modal .modal-body").html(res.html);

//            },
//            error: function (err) {
//                console.log(err);
//            }

//        })

//    } catch (e) {
//        console.log(e)

//    }
//    //Prevent Default form submit event
//    return false;
//}