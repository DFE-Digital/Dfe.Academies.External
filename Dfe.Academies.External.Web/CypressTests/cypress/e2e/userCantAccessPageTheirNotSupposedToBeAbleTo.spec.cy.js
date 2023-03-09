import { unAuthUrl, login_username, login_password } from '../../config'

import A2BLogin from '../page-objects/pages/A2BLogin'

import A2BYouDontHavePermissionsPage from '../page-objects/pages/A2BYouDontHavePermissionsPage'

describe('User Can\'t Access Application They\'re Not Supposed To', () => {

it('User Can\'t Access Application They\'re Not Supposed To', () => {

    cy.visit(unAuthUrl)

    A2BLogin.loginToUnauthApplication(login_username, login_password)

    // REMEMBER THIS TEST IS SET FOR TEST / STAGING SO HEADER AND FOOTER ELEMENT CHECKS WONT WORK
    A2BYouDontHavePermissionsPage.youDontHavePermissionsElementsVisible()


})
})