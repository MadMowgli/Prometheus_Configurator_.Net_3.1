async function showToast() {
    const toast = document.getElementById('toastEr');
    const bootstrapToast = new bootstrap.Toast(toast);
    bootstrapToast.show();
    await this.sleep(5000);
    bootstrapToast.hide();
}

function sleep(milliseconds) {
    return new Promise(resolve => setTimeout(resolve, milliseconds));
}