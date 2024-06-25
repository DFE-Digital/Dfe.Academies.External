// TODO fix commented lines
// TODO get better selectors
class Application {
  public checkTrustSectionComplete(): this {
    cy.get('strong[class="govuk-tag app-task-list__tag"]').contains('Completed')

    return this
  }

  public addTrust(): this {
    cy.get('.govuk-body').eq(0).then(($applicationId) => {
      const applicationId = $applicationId.text().split('_').pop().replace(/\s/g, '')
      cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${applicationId}"]`).click()
    })

    return this
  }

  public addSchool(): this {
    cy.get('.govuk-body').eq(0).then(($applicationId) => {
      const applicationId = $applicationId.text().split('_').pop().replace(/\s/g, '')
      cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).click()
    })

    return this
  }

  public selectTrustDetails(): this {
    cy.url().then((url) => {
      const applicationId = url.split('=').pop()
      cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${applicationId}"]`).contains('Trust details')
      cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${applicationId}"]`).click()
    })

    return this
  }

  public startAboutTheConversion(): this {
    cy.contains('About the conversion').click()

    return this
  }

  public checkAboutConversionCompleted(): this {
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')

    return this
  }

  public startFurtherInformation(): this {
    cy.contains('Further information').click()

    return this
  }

  public checkFurtherInformationCompleted(): this {
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Completed')

    return this
  }

  public startFinances(): this {
    cy.contains('Finances').click()

    return this
  }

  public checkFinanceCompleted(): this {
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Completed')

    return this
  }

  public startFuturePupilNumbers(): this {
    cy.contains('Future pupil numbers').click()

    return this
  }

  public checkFuturePupilNumbersCompleted(): this {
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Completed')

    return this
  }

  public startLandAndBuildings(): this {
    cy.contains('Land and buildings').click()

    return this
  }

  public checkLandAndBuildingsCompleted(): this {
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Completed')

    return this
  }

  public startConsultation(): this {
    cy.contains('Consultation').click()

    return this
  }

  public checkConsultationCompleted(): this {
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Completed')

    return this
  }

  public startPreopeningSupportGrant(): this {
    cy.contains('Pre-opening support grant').click()

    return this
  }

  public checkPreopeningSupportGrantCompleted(): this {
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Completed')

    return this
  }

  public startDeclaration(): this {
    cy.contains('Declaration').click()

    return this
  }

  public checkDeclarationCompleted(): this {
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Completed')

    return this
  }

  public submitApplication(): this {
    cy.get('input[type=submit]').click()

    return this
  }

  public selectCancelApplication(): this {
    cy.contains('Cancel application').click()

    return this
  }

  public inviteContributor(): this {
    cy.contains('invite or remove').click()

    return this
  }

  // Form A MAT specific
  public FAMApplicationNotStartedElementsVisible(): this {
    cy.get('.govuk-body').eq(0).then(() => {
      // const applicationId = $applicationId.text().split('_').pop().replace(/\s/g, '')

      cy.get('a[href="/your-applications"]').contains('Back')
      cy.get('p').contains('Application reference:')
      cy.get('.govuk-heading-l').contains('Form a new multi-academy trust')

      cy.get('p[class="govuk-body govuk-radios__conditional"]').contains('All school and trust details must be given before this application can be submitted.')

      cy.get('.govuk-heading-m').eq(0).contains('Give details of schools in the trust')

      // cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).should('be.visible').contains('Add a school')
      // cy.get(`a[href="/remove-school-selection?appId=${applicationId}"]`).contains('Remove a school')

      cy.get('.govuk-heading-m').eq(1).contains('Give details of the trust')

      // cy.get(`a[href="/trust/form-amat/application-new-trust-name?appId=${applicationId}"]`).should('be.visible').contains('Add the trust')
      cy.get('h2[class="govuk-heading-l"]').contains('Contributors')
      // cy.get('p').eq(3).contains('You will be able to invite other people to help you complete this form after you have added a school.')
    })

    return this
  }

  public FAMApplicationNotStartedSchoolAddedElementsVisible(): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Form a new multi-academy trust')

    cy.get('p[class="govuk-body govuk-radios__conditional"]').contains('All school and trust details must be given before this application can be submitted.')

    cy.get('.govuk-heading-m').eq(0).contains('Give details of schools in the trust')

    // cy.get(`a[href="/school/school-overview?appId=${applicationId}&urn=113537"]`).contains('Plymstock School')
    // cy.get('strong').eq(1).contains('Not Started')

    // cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).should('be.visible').contains('Add a school')
    // cy.get(`a[href="/remove-school-selection?appId=${applicationId}"]`).contains('Remove a school')

    cy.get('.govuk-heading-m').eq(1).contains('Give details of the trust')

    // cy.get(`a[href="/trust/form-amat/application-new-trust-name?appId=${applicationId}"]`).should('be.visible').contains('Add the trust')
    cy.get('h2[class="govuk-heading-l"]').contains('Contributors')
    // cy.get('p').eq(3).contains('You can invite or remove contributors to this form. If you are not the chair of the school\'s governing body, you must add them so that they can submit this application.')

    return this
  }

  public FAMApplicationSchoolCompleteElementsVisible(): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Form a new multi-academy trust')

    cy.get('p[class="govuk-body govuk-radios__conditional"]').contains('All school and trust details must be given before this application can be submitted.')

    cy.get('.govuk-heading-m').eq(0).contains('Give details of schools in the trust')

    // cy.get(`a[href="/school/school-overview?appId=${applicationId}&urn=113537"]`).contains('Plymstock School')
    cy.get('strong').eq(1).contains('Completed')

    // cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).should('be.visible').contains('Add a school')
    // cy.get(`a[href="/remove-school-selection?appId=${applicationId}"]`).contains('Remove a school')

    cy.get('.govuk-heading-m').eq(1).contains('Give details of the trust')

    // cy.get(`a[href="/trust/form-amat/application-new-trust-name?appId=${applicationId}"]`).should('be.visible').contains('Add the trust')
    cy.get('h2[class="govuk-heading-l"]').contains('Contributors')
    // cy.get('p').eq(3).contains('You can invite or remove contributors to this form. If you are not the chair of the school\'s governing body, you must add them so that they can submit this application.')

    return this
  }

  public FAMApplicationTrustNameComplete(): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Form a new multi-academy trust')

    cy.get('p[class="govuk-body govuk-radios__conditional"]').contains('All school and trust details must be given before this application can be submitted.')

    cy.get('.govuk-heading-m').eq(0).contains('Give details of schools in the trust')

    // cy.get(`a[href="/school/school-overview?appId=${applicationId}&urn=113537"]`).contains('Plymstock School')
    cy.get('strong').eq(1).contains('Completed')

    // cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).should('be.visible').contains('Add a school')
    // cy.get(`a[href="/remove-school-selection?appId=${applicationId}"]`).contains('Remove a school')

    cy.get('.govuk-heading-m').eq(1).contains('Give details of the trust')

    cy.get('h3').contains('Plymouth')
    // cy.get(`a[href="/trust/form-amat/application-new-trust-name?appId=${applicationId}"]`).contains('Change')

    // cy.get(`a[href="/trust/form-amat/application-new-trust-summary?appId=${applicationId}"]`).contains('Trust details')
    cy.get('strong').eq(2).contains('In Progress')

    cy.get('h2[class="govuk-heading-l"]').contains('Contributors')
    // cy.get('p').eq(3).contains('You can invite or remove contributors to this form. If you are not the chair of the school\'s governing body, you must add them so that they can submit this application.')

    return this
  }

  public FAMApplicationFuturePupilNumbersSubmittedElementsVisible(): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Form a new multi-academy trust')

    cy.get('p[class="govuk-body govuk-radios__conditional"]').contains('All school and trust details must be given before this application can be submitted.')

    cy.get('.govuk-heading-m').eq(0).contains('Give details of schools in the trust')

    // cy.get(`a[href="/school/school-overview?appId=${applicationId}&urn=113537"]`).contains('Plymstock School')
    cy.get('strong').eq(1).contains('In Progress')

    // cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).should('be.visible').contains('Add a school')
    // cy.get(`a[href="/remove-school-selection?appId=${applicationId}"]`).contains('Remove a school')

    cy.get('.govuk-heading-m').eq(1).contains('Give details of the trust')

    // cy.get(`a[href="/trust/form-amat/application-new-trust-name?appId=${applicationId}"]`).should('be.visible').contains('Add the trust')
    cy.get('h2[class="govuk-heading-l"]').contains('Contributors')
    // cy.get('p').eq(3).contains('You can invite or remove contributors to this form. If you are not the chair of the school\'s governing body, you must add them so that they can submit this application.')

    return this
  }

  public selectFAMSchool(applicationId: string): this {
    cy.url().then((url) => {
      applicationId = url.split('=').pop()
      cy.get(`a[href="/school/school-overview?appId=${applicationId}&urn=113537"]`).click()
    })

    return this
  }

  public selectFAMAddTheTrust(applicationId: string): this {
    cy.url().then((url) => {
      applicationId = url.split('=').pop()
      cy.get(`a[href="/trust/form-amat/application-new-trust-name?appId=${applicationId}"]`).click()
    })

    return this
  }

  public selectFAMTrustDetails(applicationId: string): this {
    cy.url().then((url) => {
      applicationId = url.split('=').pop()
      cy.get(`a[href="/trust/form-amat/application-new-trust-summary?appId=${applicationId}"]`).click()
    })

    return this
  }

  public FAMApplicationOverviewCompleteElementsVisible(): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Form a new multi-academy trust')

    cy.get('.govuk-heading-m').eq(0).contains('Give details of schools in the trust')

    // cy.get(`a[href="/school/school-overview?appId=${applicationId}&urn=113537"]`).contains('Plymstock School')
    cy.get('strong').eq(1).contains('Completed')

    // cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).should('be.visible').contains('Add a school')
    // cy.get(`a[href="/remove-school-selection?appId=${applicationId}"]`).contains('Remove a school')

    cy.get('.govuk-heading-m').eq(1).contains('Give details of the trust')

    cy.get('h3').contains('Plymouth')
    // cy.get(`a[href="/trust/form-amat/application-new-trust-name?appId=${applicationId}"]`).contains('Change')

    // cy.get(`a[href="/trust/form-amat/application-new-trust-summary?appId=${applicationId}"]`).contains('Trust details')
    cy.get('strong').eq(2).contains('Completed')

    cy.get('h2[class="govuk-heading-l"]').contains('Contributors')
    // cy.get('p').eq(3).contains('You can invite or remove contributors to this form. If you are not the chair of the school\'s governing body, you must add them so that they can submit this application.')

    cy.get('input[type=submit]').should('be.visible').contains('Submit application')

    return this
  }
}

const application = new Application()

export default application
