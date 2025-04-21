import Vuex from 'vuex';
import Cookies from "js-cookie";
const Token_Key = "token"
const RefreshToken_Key = "refreshToken"
const Account_Key = "account"
const store = new Vuex.Store({
    state: {
        token: Cookies.get(Token_Key) ?? null,
        refreshToken: Cookies.get(RefreshToken_Key) ?? null,
        account: localStorage.getItem(Account_Key) ?? null,
        excelData: null,
        loading: false,
    },
    mutations: {
        setToken(state, token) {
            Cookies.set(Token_Key, token, {
                expires: 7,
                path: "",
            });
            state.token = token;
        },
        setLoading(state, loading) {
            state.loading = loading;
        },
        setRefreshToken(state, refreshToken) {
            // var inThreeMinutes = new Date(new Date().getTime() + 3 * 60 * 1000);
            Cookies.set(RefreshToken_Key, refreshToken, {
                expires: 30,
                // path: "/api/v1/Accounts/RefreshToken",
            });
            state.refreshToken = refreshToken;
        },
        setAccount(state, account) {
            localStorage.setItem(Account_Key, JSON.stringify(account));
            state.account = JSON.stringify(account);
        },
        setExcelData(state, data) {
            state.excelData = data;
        },
        clearToken(state) {
            Cookies.remove(Token_Key);
            Cookies.remove(RefreshToken_Key);
            localStorage.removeItem(Account_Key);
            state.token = null
            state.refreshToken = null
            state.account = null
            // Clear token from session cookie
        },
    },
    actions: {

        // Action to set the token
        setToken({ commit }, token) {
            commit('setToken', token);
        },
        setLoading({ commit }, loading) {
            commit('setLoading', loading);
        },
        setRefreshToken({ commit }, refreshToken) {
            commit('setRefreshToken', refreshToken);
        },
        setAccount({ commit }, account) {
            commit('setAccount', account);
        },
        // Action to clear the token (e.g., logout)
        clearToken({ commit }) {
            commit('clearToken');
        },
    },
    getters: {
        getExcelData: state => {
            return state.excelData;
        },
        getLoading: state => {
            return state.loading;
        },
        getToken(state) {
            return state.token;
        },
        getRefreshToken(state) {
            return state.refreshToken;
        },
        getAccount(state) {
            return state.account;
        },

        isAuthenticated(state) {
            return !!state.token;
        }
    },
});

export default store;
