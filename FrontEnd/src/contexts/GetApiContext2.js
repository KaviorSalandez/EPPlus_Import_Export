// Import store và router
import store from '../store/store';
import { handleRefreshToken } from '../services/EmployeeService';
import router from '../router'
/*
Tên hàm: Hàm lấy dữ liệu API sử dụng token
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2023
*/
const refreshToken = async (api, ...parameters) => {
    // Lấy access token từ store
    const accessToken = store.getters.getToken;
    // Tạo object token
    const token = {
        headers: {
            Authorization: `Bearer ${accessToken}`,
        },
    };
    store.dispatch('setLoading', true);
    try {
        // Gọi API với token
        const response = await api(token, ...parameters);
        // Trả về response nếu thành công
        store.dispatch('setLoading', false);
        return response;
    } catch (error) {
        // Xử lý lỗi 401 (Unauthorized)
        if (error.status === 401) {
            // Tạo body request
            const body = {
                AccessToken: accessToken,
                RefreshToken: store.getters.getRefreshToken, // Lấy refresh token từ store
            };
            try {
                // Gọi service refresh token
                const refreshResponse = await handleRefreshToken(body);
                // Cập nhật token mới vào store
                store.dispatch('setToken', refreshResponse.data.AccessToken);
                store.dispatch('setRefreshToken', refreshResponse.data.RefreshToken);
                // Gọi lại API ban đầu với token mới (đệ quy)
                return refreshToken(api, ...parameters);
            } catch (refreshError) {
                store.dispatch('setLoading', false);
                // Xóa token và chuyển hướng về trang login
                store.dispatch('clearToken');
                router.push('/login');
                window.location.reload();
                return refreshError;
            }
        }
        console.log(error);
        return error;
    }
};
// Xuất hàm
export default refreshToken;
