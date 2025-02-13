window.openFileDialog = (inputId) => {
    const fileInput = document.getElementById(inputId);
    if (fileInput) {
        fileInput.click();
    }
};