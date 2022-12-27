import { configureStore } from '@reduxjs/toolkit'
import {accountReducer} from './reducers/account'

const store = configureStore({
    reducer: {
        account: accountReducer
    },
})

export default store;