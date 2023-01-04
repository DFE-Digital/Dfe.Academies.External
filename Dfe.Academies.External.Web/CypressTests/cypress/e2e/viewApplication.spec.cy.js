import { url, login_username, login_password } from '../../config'
import CookieHeaderModal from '../page-objects/components/CookieHeaderModal'
import A2BHome from '../page-objects/pages/A2BHome'
import A2BLogin from '../page-objects/pages/A2BLogin'
import A2BYourApplicationNotStarted from '../page-objects/pages/A2BYourApplicationNotStarted'
import A2BYourApplications from '../page-objects/pages/A2BYourApplications'

describe('View Application Tests', () => {

    beforeEach(function() {
        cy.visit(url)
        CookieHeaderModal.clickAcceptAnalyticsCookies()
        A2BHome.StartNowVisible()
        A2BHome.clickStartNow()
      })

    it('should be able to view a Not Started Application', () => {
        A2BLogin.login(login_username, login_password)
        A2BYourApplications.yourApplicationsElementsVisible()
        A2BYourApplications.selectJAMNotStartedApplication()
        A2BYourApplicationNotStarted.yourApplicationNotStartedElementsVisible()
    })
})