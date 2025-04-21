/*
Tên hàm: Định dạng ngày tháng năm
Tham số: date là Ngày được gưỉ đến, format là loại muốn convert 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023

*/
export const format_date = (date, format = "yyyy-mm-dd") => {
    try {
        if (typeof date !== "string" || typeof format !== "string") return;

        const [year, month, day] = date.split("T")[0].split("-");

        switch (format) {
            case "dd-mm-yyyy":
                return `${day}/${month}/${year}`;
            case "mm-dd-yyyy":
                return `${month}/${day}/${year}`;
            case "yyyy-mm-dd":
                return `${year}-${month}-${day}`;
            default:
                break;
        }
    } catch (error) {
        console.error(error);
    }
}