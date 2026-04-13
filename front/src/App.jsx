import { useContext, useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { Route, Routes } from 'react-router'
import DefaultLayout from './components/layout/DefaultLayout'
import MainPage from './pages/mainPage/MainPage'
import ErrorPage from './pages/errorPage/ErrorPage'
import { AuthContext } from './pages/context/AuthContext'
import { ThemeContext } from './pages/context/ThemeContext'
import { ThemeProvider } from '@emotion/react'
import { darkTheme } from './pages/theme/darkTheme'
import { lightTheme } from './pages/theme/lightTheme'
import CarListPage from './pages/carPage/CarListPage'
import CarCreateForm from './pages/carPage/CarCreateForm'
import CarUpdateForm from './pages/carPage/CarUpdateForm'
import CarDescription from './pages/carPage/CarDescription'

function App() {

  const { login } = useContext(AuthContext);
  const [role, setRole] = useState("admin");
  const { isDark } = useContext(ThemeContext);

  return (
    <>
      <ThemeProvider theme={isDark ? darkTheme : lightTheme}>
        <Routes>
          <Route path='/' element={<DefaultLayout />}>
            <Route index element={<MainPage />} />

            <Route path='/cars'>
              <Route index element={<CarListPage />} />
              <Route path='create' element={<CarCreateForm/>}/>
              <Route path='update/:id' element={<CarUpdateForm/>}/>
              <Route path='description/:id' element={<CarDescription/>}/>
            </Route>

            <Route path='*' element={<ErrorPage />} />
          </Route>
        </Routes>
      </ThemeProvider>
    </>
  )
}

export default App
