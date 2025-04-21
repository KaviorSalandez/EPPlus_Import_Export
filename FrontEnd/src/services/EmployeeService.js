import axios from 'axios';

/*
Tên hàm: Tạo ra mã code mới 
created at: 15/1/2024
created by: Đặng Đình Quốc Khánh
*/
export const createNewCode = async (token) => {
    try {
        const newCode = await axios.get(
            `https://localhost:7061/api/v1/Employees/GenerateCode`, token
        );
        return newCode.data;
    } catch (err) {
        throw err.response;
    }
};
/*
Tên hàm: tạo ra nhân viên mới
param: truyền vào một employee 
created at: 15/1/2024
created by: Đặng Đình Quốc Khánh
*/
export const createOne = async (token, body) => {
    try {
        let res = await axios.post(
            `https://localhost:7061/api/v1/Employees`, body, token
        );
        return res;
    } catch (error) {
        throw error.response
    }

};

/*
Tên hàm: cập nhật nhân viên
param: truyền vào một nhân viên và id của nhân viên đó
created at: 15/1/2024
created by: Đặng Đình Quốc Khánh
*/
export const updateOne = async (token, body, id) => {
    try {
        let res = await axios.put(
            `https://localhost:7061/api/v1/Employees/${id}`, body, token
        );
        return res;
    } catch (error) {
        throw error.response
    }

};

/*
Tên hàm: xóa 1 nhân viên 
param: id của nhân viên đó
created at: 15/1/2024
created by: Đặng Đình Quốc Khánh
*/
export const deleteOne = async (token, id) => {
    try {
        let res = await axios.delete(
            `https://localhost:7061/api/v1/Employees/${id}`, token
        );
        return res;
    } catch (error) {
        throw error.response
    }

};

/*
Tên hàm: tìm nhân viên 
param: id của nhân viên đó
created at: 15/1/2024
created by: Đặng Đình Quốc Khánh
*/
export const findJoin = async (token, id) => {
    try {
        let res = await axios.get(
            `https://localhost:7061/api/v1/Employees/FindJoin/${id}`, token
        );
        return res.data;
    } catch (error) {
        throw error.response
    }

};
/*
Tên hàm: Đăng nhập tài khoản 
param: thông tin username và password trong body 
created at: 15/1/2024
created by: Đặng Đình Quốc Khánh
*/
export const signin = async (body) => {
    // https://localhost:7061/api/v1/Account/signin?email=khanhddq13.11.2002%40gmail.com&password=khanh123%40
    try {
        let res = await axios.post(
            `https://localhost:7061/api/v1/Accounts/Login`, body
        );
        return res;
    } catch (error) {
        throw error.response
    }

};


/*
Tên hàm: Refresh token 
param: thông tin access token và refresh token
created at: 15/1/2024
created by: Đặng Đình Quốc Khánh
*/
export const handleRefreshToken = async (body) => {
    // https://localhost:7061/api/v1/Account/signin?email=khanhddq13.11.2002%40gmail.com&password=khanh123%40
    try {
        let res = await axios.post(
            `https://localhost:7061/api/v1/Accounts/RefreshToken`, body
        );
        return res;
    } catch (error) {
        throw error.response
    }

};
/*
Tên hàm: lấy ra tất cả nhân viên và đơn vị, vị trí nhân viên đó làm việc 
param: kích thước trang, trang số bao nhiêu, và chuốĩ tìm kiếm theo mã nhân viên, tên nhân viên, và số cmnd
created at: 15/1/2024
created by: Đặng Đình Quốc Khánh
*/
export const findAndFilter = async (token, pageSize, pageNumber, search) => {
    try {
        let res = await axios.get(
            `https://localhost:7061/api/v1/Employees/Filter?pageSize=${pageSize}&pageNumber=${pageNumber}&search=${search}`, token
        );
        return res;
    } catch (error) {
        throw error.response
    }
};
/*
Tên hàm: lấy ra tất cả nhân viên
created at: 15/1/2024
created by: Đặng Đình Quốc Khánh
*/
export const findAll = async () => {
    try {
        let res = await axios.get(
            `https://localhost:7061/api/v1/Employees/Filter?pageSize=0`
        );
        return res;
    } catch (error) {
        throw error.response
    }

};

/*
Tên hàm: Xóa nhiều nhân viên 
param: truyền vào các id của nhân viên đó
created at: 15/1/2024
created by: Đặng Đình Quốc Khánh
*/
export const deleteMany = async (token, query) => {
    try {
        let res = await axios.delete(
            `https://localhost:7061/api/v1/Employees/DeleteMany?${query}`, token
        );
        return res;
    } catch (error) {
        throw error.response
    }
};


/*
Tên hàm: nhập khẩu danh sách nhân viên 
param: file excel nhập khẩu
created at: 17/1/2024
created by: Đặng Đình Quốc Khánh
*/

export const importEmployee = async (token, formData) => {
    try {
        let res = await axios.post(
            "https://localhost:7061/api/v1/Employees/ImportValidate",
            formData,
            {
                headers: {
                    "Content-Type": "multipart/form-data", // Ensure the correct content type is set
                    "Authorization": `Bearer ${token}`
                },
            }
        );
        return res;
    } catch (error) {
        throw error.response
    }

};
/*
Tên hàm: nhập khẩu danh sách nhân viên thêm vào database 
param: file excel nhập khẩu
created at: 17/1/2024
created by: Đặng Đình Quốc Khánh
*/

export const importEmployeeDatabase = async (token, idImport) => {
    try {
        let res = await axios.get(
            `https://localhost:7061/api/v1/Employees/ImportExcel/${idImport}`, token
        );
        return res;
    } catch (error) {
        throw error.response
    }

};
/*
Tên hàm: export dữ liệu raw của bảng employee
created at: 17/1/2024
created by: Đặng Đình Quốc Khánh
*/

export const exportRaw = async (token) => {
    try {
        let res = await axios.get(
            "https://localhost:7061/api/v1/Employees/ExportExcel?checkData=0", {

            responseType: "arraybuffer", ...token
        },);
        return res;
    } catch (error) {
        throw error.response
    }

};
/*
Tên hàm: export dữ liệu bảng nhân viên của bảng employee
created at: 17/1/2024
created by: Đặng Đình Quốc Khánh
*/

export const exportEmployees = async (token, checkData) => {
    try {
        // checkData = 1 là lấy tất cả
        // còn không thì Truyền Ids là lấy tất
        let res = await axios.get(
            `https://localhost:7061/api/v1/Employees/ExportExcel?checkData=${checkData}`, {
            responseType: "arraybuffer", ...token
        });
        return res;
    } catch (error) {
        throw error.response
    }

};
/*
Tên hàm: export dữ liệu nhân viên  được chonj 
created at: 17/1/2024
created by: Đặng Đình Quốc Khánh
*/

export const exportEmployee = async (token, body) => {
    try {
        // checkData = 1 là lấy tất cả
        // còn không thì Truyền Ids là lấy tất
        let res = await axios.post(
            `https://localhost:7061/api/v1/Employees/ExportExcel`, body, {
            responseType: "arraybuffer", ...token
        });
        return res;
    } catch (error) {
        throw error.response
    }

};
