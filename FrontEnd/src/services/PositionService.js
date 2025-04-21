import axios from 'axios';

/*
Tên hàm: Lấy ra tất cả chức vụ
created at: 15/1/2024
created by: Đặng Đình Quốc Khánh
*/

export const getAllPosition = async (token) => {

    try {
        const positions = await axios.get(
            `https://localhost:7061/api/v1/Positions`, token
        );
        return positions.data;
    } catch (err) {
        throw err.response.data;
    }
};