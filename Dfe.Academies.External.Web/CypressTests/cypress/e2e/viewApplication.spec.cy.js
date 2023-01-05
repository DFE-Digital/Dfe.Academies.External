import { url, login_username, login_password } from '../../config'
import CookieHeaderModal from '../page-objects/components/CookieHeaderModal'
import A2BHome from '../page-objects/pages/A2BHome'
import A2BLogin from '../page-objects/pages/A2BLogin'
import A2BYourApplication from '../page-objects/pages/A2BYourApplication'
import A2BYourApplications from '../page-objects/pages/A2BYourApplications'

describe('View Application Tests', () => {

    beforeEach(function() {
        cy.visit(url)
        A2BHome.govUkHeaderVisible()
        A2BHome.applyToBecomeAnAcademyHeaderLinkVisible()
        A2BHome.h1ApplyToBecomeAnAcademyVisible()
        A2BHome.p3Visible()
        A2BHome.p4Visible()
        A2BHome.checkSpecialSchoolsLinkVisible()
        A2BHome.checkPupilReferralUnitsLinkVisible()
        A2BHome.p6Visible()
        A2BHome.h2Visible()
        A2BHome.p7Visible()
        A2BHome.completeAnEqualityImpactAssessmentVisible()
        A2BHome.consultationWithStakeholdersLinkVisible()
        A2BHome.p8Visible()
        A2BHome.contactYourRegionalDirectorLinkVisible()
        A2BHome.allInformationAndEvidenceYouWillNeedLinkVisible()
        CookieHeaderModal.clickAcceptAnalyticsCookies()
        A2BHome.StartNowVisible()
        A2BHome.clickStartNow()
      })

    it('should be able to view a Not Started Application', () => {
        A2BLogin.login(login_username, login_password)
        A2BYourApplications.yourApplicationsElementsVisible()
        A2BYourApplications.selectJAMNotStartedApplication()
        A2BYourApplication.yourApplicationNotStartedElementsVisible()
    })
})