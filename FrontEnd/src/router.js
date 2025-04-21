
import ImportData from './layouts/imports/ImportData.vue'
import TableDataImport from './layouts/imports/TableDataImport.vue'
import ResultImport from './layouts/imports/ResultImport.vue'
import EmployeeImport from './pages/EmployeeImport.vue'
import Home from '../src/pages/Home.vue'
import Login from '../src/pages/Login.vue'
import store from '../src/store/store'

import EmployeeList from './pages/EmployeeList.vue';
import { createRouter, createWebHashHistory } from 'vue-router'
const routes = [
    { path: '/', redirect: "/home" },
    { path: '/login', component: Login, meta: { requireUnauth: true } },
    {
        path: '/home', component: Home, redirect: '/home/table-employee', meta: { requiresAuth: true }, children: [
            { name: 'table-employee', path: 'table-employee', component: EmployeeList, props: true },
        ]
    },
    {
        path: '/import', component: EmployeeImport, redirect: '/import/import-data', meta: { requiresAuth: true },
        children: [
            { name: 'import-data', path: 'import-data', component: ImportData, props: true },
            { name: 'table-data', path: 'table-data', component: TableDataImport, props: true },
            { name: 'result-data', path: 'result-data', component: ResultImport, props: true },
        ]
    },
]
const router = createRouter({
    history: createWebHashHistory(),
    routes,
    linkActiveClass: "active-router",
    linkExactActiveClass: "exact-active",
})
router.beforeEach(function (to, _, next) {
    console.log(to.meta.requiresAuth, store.getters.isAuthenticated, to.meta.requiresUnauth);
    if (to.meta.requiresAuth && !store.getters.isAuthenticated) {
        next('/login');
    } else if (to.meta.requiresUnauth && store.getters.isAuthenticated) {
        next('/home')
    }
    else {
        next();
    }
})

export default router;