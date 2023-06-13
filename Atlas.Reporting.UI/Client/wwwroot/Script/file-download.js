    window.BlazorDownloadFile = function (fileName, mimeType, fileContent) {
        const blob = new Blob([fileContent], {type: mimeType });

    const link = document.createElement("a");
    link.href = window.URL.createObjectURL(blob);
    link.download = fileName;


    document.body.appendChild(link);

    link.click();

    document.body.removeChild(link);
    }