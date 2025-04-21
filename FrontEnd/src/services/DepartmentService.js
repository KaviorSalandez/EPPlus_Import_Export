import axios from 'axios';

/*
Tên hàm: lấy ra tất cả các đơn vị 
created at: 15/1/2024
created by: Đặng Đình Quốc Khánh
*/
export const getAllDepartment = async (token) => {

    try {
        const departments = await axios.get(
            `https://localhost:7061/api/v1/Departments`, token
        );
        return departments.data;
    } catch (err) {
        throw err.response.data;
    }
};