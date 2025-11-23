import { Navigate, Route, Routes } from 'react-router'

import { MainLayout } from '@/layouts/MainLayout'

import { Home } from '@/pages/Home'
import { LoginPage } from '@/pages/LoginPage'
import { LogoutPage } from '@/pages/LogoutPage'
import { MovieDetailsPage } from '@/pages/MovieDetailsPage'
import { MoviesPage } from '@/pages/MoviesPage'
import { PeoplePage } from '@/pages/PeoplePage'
import { PersonDetailsPage } from '@/pages/PersonDetailsPage'
import { RegisterPage } from '@/pages/RegisterPage'
import { SearchPage } from '@/pages/SearchPage'

import './App.css'

export const App = () => {
  return (
    <Routes>
      <Route element={<MainLayout />}>
        <Route index element={<Home />} />
        <Route path='movies' element={<MoviesPage />} />
        <Route path='movies/:movieId' element={<MovieDetailsPage />} />
        <Route path='people' element={<PeoplePage />} />
        <Route path='people/:personId' element={<PersonDetailsPage />} />
        <Route path='search' element={<SearchPage />} />
        <Route path='login' element={<LoginPage />} />
        <Route path='register' element={<RegisterPage />} />
        <Route path='logout' element={<LogoutPage />} />
        <Route path='*' element={<Navigate to='/' replace />} />
      </Route>
    </Routes>
  )
}
