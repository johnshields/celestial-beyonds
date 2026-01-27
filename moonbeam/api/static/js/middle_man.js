$("#talk").click(function () {
    let value = jQuery("#user_input").val();
    const userInput = {value: value};
    console.log("User:");
    console.log(userInput);

    $.ajax("/api/chat", {
        type: "POST",
        // take the entered value
        data: userInput,
        success: function (data) {
            JSON.stringify(data)
            $("#bot_response").val(`${data}`);
            console.log("Moonbeam:" + data);
        },
        error: function (error) {
            console.error("failed to respond." + error);
        },
    });
})