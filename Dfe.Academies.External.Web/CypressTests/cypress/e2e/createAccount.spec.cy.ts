import { url } from '../../config'
import Header from '../page-objects/components/Header'
import CookieHeaderModal from '../page-objects/components/CookieHeaderModal'
import A2BHome from '../page-objects/pages/A2BHome'
import A2BLogin from '../page-objects/pages/A2BLogin'
import A2BCreateAnAccount from '../page-objects/pages/A2BCreateAnAccount'
import A2BCreateAnAccountConfirm from '../page-objects/pages/A2BCreateAnAccountConfirm'
import Footer from '../page-objects/components/Footer'

describe('Create Account Tests', () => {

  beforeEach(function() {
    cy.visit(url)

    Header.govUkHeaderVisible()
    Header.applyToBecomeAnAcademyHeaderLinkVisible()

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

    Footer.accessibilityStatementLinkVisible()
    Footer.cookiesLinkVisible()
    Footer.termsAndConditionsLinkVisible()
    Footer.privacyLinkVisible()
    Footer.oglLogoVisible()
    Footer.allContentTextVisible()
    Footer.openGovernmentLicence3LinkVisible()
    Footer.crownCopyrightLinkVisible()

    CookieHeaderModal.clickAcceptAnalyticsCookies()
    A2BHome.StartNowVisible()
    A2BHome.clickStartNow()
  })

  it('should fail to create an account just clicking submit', () => {
    A2BLogin.createAccount()

    A2BCreateAnAccount.createAccountElementsVisible()

    A2BCreateAnAccount.createAccountFailsWithNoData()
  })

  it('should Create an Account Successfully', () => {
    A2BLogin.createAccount()

    A2BCreateAnAccount.createAccountElementsVisible()

    A2BCreateAnAccount.createAccountSuccessful()

    A2BCreateAnAccountConfirm.createAccountConfirmElementsVisible()
    
  })
})