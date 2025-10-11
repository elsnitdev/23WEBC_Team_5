$(document).ready(() => {
    // Hàm định dạng giá tiền
    const formatPrice = (price) => {
        const safePrice = typeof price === 'number' && !isNaN(price) ? price : 0;
        const priceString = safePrice.toFixed(2).replace(/./g, (ch, index, rootString) => {
            return index > 0 && ch !== "." && (rootString.length - index) % 3 === 0 ? "," + ch : ch;
        });
        return priceString + " VNĐ";
    }

    const apiUrl = "/api/products?itemsPerPage=8";
    const container = $('#product-container');
    const loading = $('#loading-indicator');

    $.ajax({
        url: apiUrl,
        method: "GET",
        dataType: "json",
        success: (data) => {
            if (data && data.length > 0) {
                $.each(data, (index, product) => {
                    const giaCuoiCung = product.donGia * (1 - product.khuyenMai/100.0);
                    const productHtml = `
                        <div class="col-md-3 feature-grid">
                            <a href="/Products/Details/${product.maSP}">
                                <img src="${product.hinhAnh ? '/images/' + product.hinhAnh.split(";")[0] : '/images/default.jpg'}" alt="${product.tenSP}" />
                                <div class="arrival-info">
                                    <h4>${product.tenSP}</h4>
                                    <p>${formatPrice(giaCuoiCung)}</p>
                                    ${product.khuyenMai > 0
                            ? `<span class="pric1"><del>${formatPrice(product.donGia)}</del></span>
                                            <span class="disc">[${product.khuyenMai}% Off]</span>`
                            : ''}
                                </div>
                                <div class="viw">
                                    <a href="/Products/Details/${product.maSP}"><span class="glyphicon glyphicon-eye-open" aria-hidden="true"></span>View</a>
                                </div>
                            </a>
                        </div>
                    `;
                    container.append(productHtml);
                }); 
                container.append('<div class="clearfix"></div>');
            } else {
                container.html('<p style="text-align: center;">Không tìm thấy sản phẩm nào.</p>');
            }
        },
        error: function (xhr, status, error) {
            console.error("Lỗi tải sản phẩm:", error);
            container.html('<p style="text-align: center; color: red;">Đã xảy ra lỗi khi tải sản phẩm.</p>');
        },
        complete: function () {
            // Ẩn loading indicator sau khi yêu cầu hoàn tất
            loading.hide();
        }
    })
})