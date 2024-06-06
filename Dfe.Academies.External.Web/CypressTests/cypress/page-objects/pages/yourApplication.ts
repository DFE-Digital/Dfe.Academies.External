class YourApplication {
  public yourApplicationNotStartedElementsVisible(): this {
    cy.get('.govuk-body').eq(0).then(($applicationId) => {
      const applicationId = $applicationId.text().split('_').pop().replace(/\s/g, '')

      cy.get('a[href="/your-applications"]').contains('Back')
      cy.get('p').contains('Application reference:')
      cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
      cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
      cy.get('h2').contains('The school applying to convert')

      cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).should('be.visible').contains('Add a school')

      // cy.get('h2').eq(1).contains('The trust the school will join')
      cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${applicationId}"]`).should('be.visible').contains('Add a trust')
      cy.get('h2[class=govuk-heading-l]').contains('Contributors')
      // cy.get('p').eq(3).contains('You will be able to invite other people to help you complete this form after you have added a school.')
    })

    return this
  }

  public yourApplicationNotStartedButSchoolAddedElementsVisible(applicationId: string): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).contains('Change')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${applicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${applicationId}"]`).contains('Trust details')
    cy.get('strong[class=govuk-tag app-task-list__tag govuk-tag--blue]').contains('In Progress')

    cy.get('h2[class=govuk-heading-l]').contains('Contributors')
    // cy.get('p').eq(4).contains('You can invite or remove contributors to this form. If you are not the chair of the school\'s governing body, you must add them so that they can submit this application.')

    return this
  }

  public yourApplicationNotStartedButTrustSectionCompleteElementsVisible(applicationId: string): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).contains('Change')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${applicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${applicationId}"]`).contains('Trust details')
    cy.get('strong[class=govuk-tag app-task-list__tag]').contains('Completed')

    return this
  }

  public selectAddATrust(): this {
    cy.get('.govuk-body').eq(0).then(($applicationId) => {
      const applicationId = $applicationId.text().split('_').pop().replace(/\s/g, '')
      cy.log(`Global Application ID = ${applicationId}`)
      cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${applicationId}"]`).click()
    })

    return this
  }

  public selectAddASchool(): this {
    cy.get('.govuk-body').eq(0).then(($applicationId) => {
      const applicationId = $applicationId.text().split('_').pop().replace(/\s/g, '')
      cy.log(`Global Application ID = ${applicationId}`)
      cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).click()
    })

    return this
  }

  public selectTrustDetails(applicationId: string): this {
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${applicationId}"]`).contains('Trust details')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${applicationId}"]`).click()

    return this
  }

  public selectChangeSchool(applicationId: string): this {
    cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).click()

    return this
  }

  public selectChangeTrust(applicationId: string): this {
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${applicationId}"]`).click()

    return this
  }

  public selectAboutTheConversion(): this {
    cy.contains('About the conversion').click()

    return this
  }

  public yourApplicationTrustSectionAndAboutConversionCompleteElementsVisible(applicationId: string): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).contains('Change')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${applicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${applicationId}"]`).contains('Trust details')
    cy.get('strong[class=govuk-tag app-task-list__tag]').contains('Completed')

    return this
  }

  public selectFurtherInformation(): this {
    cy.contains('Further information').click()

    return this
  }

  public yourApplicationTrustSectionAboutConversionFurtherInformationCompleteElementsVisible(applicationId: string): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).contains('Change')

    cy.get('div[class=govuk-grid-row]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    cy.get('h3').contains('PLYMOUTH CAST')

    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${applicationId}"]`).contains('Change')

    // TODO only one of these is right so figure out which
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${applicationId}"]`).contains('Trust details')
    // cy.get(`a[href="S"]`).contains('Trust details')

    cy.get('strong[class=govuk-tag app-task-list__tag]').contains('Completed')

    return this
  }

  public selectFinances(): this {
    cy.contains('Finances').click()

    return this
  }

  public financeCompleteElementsVisible(applicationId: string): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).contains('Change')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${applicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${applicationId}"]`).contains('Trust details')
    cy.get('strong[class=govuk-tag app-task-list__tag]').contains('Completed')

    return this
  }

  public selectFuturePupilNumbers(): this {
    cy.contains('Future pupil numbers').click()

    return this
  }

  public futurePupilNumbersCompleteElementsVisible(applicationId: string): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).contains('Change')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${applicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${applicationId}"]`).contains('Trust details')
    cy.get('strong[class=govuk-tag app-task-list__tag]').contains('Completed')

    return this
  }

  public selectLandAndBuildings(): this {
    cy.contains('Land and buildings').click()

    return this
  }

  public landAndBuildingsCompleteElementsVisible(applicationId: string): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).contains('Change')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${applicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${applicationId}"]`).contains('Trust details')
    cy.get('strong[class=govuk-tag app-task-list__tag]').contains('Completed')

    return this
  }

  public selectConsultation(): this {
    cy.contains('Consultation').click()

    return this
  }

  public consultationCompleteElementsVisible(applicationId: string): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).contains('Change')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${applicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${applicationId}"]`).contains('Trust details')
    cy.get('strong[class=govuk-tag app-task-list__tag]').contains('Completed')

    return this
  }

  public selectPreopeningSupportGrant(): this {
    cy.contains('Pre-opening support grant').click()

    return this
  }

  public preopeningSupportGrantCompleteElementsVisible(applicationId: string): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).contains('Change')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${applicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${applicationId}"]`).contains('Trust details')
    cy.get('strong[class=govuk-tag app-task-list__tag]').contains('Completed')

    return this
  }

  public selectDeclaration(): this {
    cy.contains('Declaration').click()

    return this
  }

  public declarationCompleteElementsVisible(applicationId: string): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).contains('Change')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Completed')
    cy.get('div[class=govuk-grid-row]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Completed')

    cy.get('h2').eq(1).contains('The trust the school will join')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${applicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${applicationId}"]`).contains('Trust details')
    cy.get('strong[class=govuk-tag app-task-list__tag]').contains('Completed')

    cy.get('h2[class=govuk-heading-l]').contains('Contributors')
    // cy.get('p').eq(4).contains('You can invite or remove contributors to this form. If you are not the chair of the school\'s governing body, you must add them so that they can submit this application.')

    // CHECK FOR SUBMIT APPLICATION BUTTON
    cy.contains('Submit application').should('be.visible').contains('Submit application')

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

  public selectInviteContributorLink(): this {
    cy.contains('invite or remove').click()

    return this
  }

  // Form A MAT specific
  public FAMApplicationNotStartedElementsVisible(): this {
    cy.get('.govuk-body').eq(0).then(($applicationId) => {
      const applicationId = $applicationId.text().split('_').pop().replace(/\s/g, '')

      cy.get('a[href="/your-applications"]').contains('Back')
      cy.get('p').contains('Application reference:')
      cy.get('.govuk-heading-l').contains('Form a new multi-academy trust')

      cy.get('p[class="govuk-body govuk-radios__conditional"]').contains('All school and trust details must be given before this application can be submitted.')

      cy.get('.govuk-heading-m').eq(0).contains('Give details of schools in the trust')

      cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).should('be.visible').contains('Add a school')
      cy.get(`a[href="/remove-school-selection?appId=${applicationId}"]`).contains('Remove a school')

      cy.get('.govuk-heading-m').eq(1).contains('Give details of the trust')

      cy.get(`a[href="/trust/form-amat/application-new-trust-name?appId=${applicationId}"]`).should('be.visible').contains('Add the trust')
      cy.get('h2[class=govuk-heading-l]').contains('Contributors')
      // cy.get('p').eq(3).contains('You will be able to invite other people to help you complete this form after you have added a school.')
    })

    return this
  }

  public FAMApplicationNotStartedSchoolAddedElementsVisible(applicationId: string): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Form a new multi-academy trust')

    cy.get('p[class="govuk-body govuk-radios__conditional"]').contains('All school and trust details must be given before this application can be submitted.')

    cy.get('.govuk-heading-m').eq(0).contains('Give details of schools in the trust')

    cy.get(`a[href="/school/school-overview?appId=${applicationId}&urn=113537"]`).contains('Plymstock School')
    cy.get('strong').eq(1).contains('Not Started')

    cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).should('be.visible').contains('Add a school')
    cy.get(`a[href="/remove-school-selection?appId=${applicationId}"]`).contains('Remove a school')

    cy.get('.govuk-heading-m').eq(1).contains('Give details of the trust')

    cy.get(`a[href="/trust/form-amat/application-new-trust-name?appId=${applicationId}"]`).should('be.visible').contains('Add the trust')
    cy.get('h2[class=govuk-heading-l]').contains('Contributors')
    // cy.get('p').eq(3).contains('You can invite or remove contributors to this form. If you are not the chair of the school\'s governing body, you must add them so that they can submit this application.')

    return this
  }

  public FAMApplicationSchoolCompleteElementsVisible(applicationId: string): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Form a new multi-academy trust')

    cy.get('p[class="govuk-body govuk-radios__conditional"]').contains('All school and trust details must be given before this application can be submitted.')

    cy.get('.govuk-heading-m').eq(0).contains('Give details of schools in the trust')

    cy.get(`a[href="/school/school-overview?appId=${applicationId}&urn=113537"]`).contains('Plymstock School')
    cy.get('strong').eq(1).contains('Completed')

    cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).should('be.visible').contains('Add a school')
    cy.get(`a[href="/remove-school-selection?appId=${applicationId}"]`).contains('Remove a school')

    cy.get('.govuk-heading-m').eq(1).contains('Give details of the trust')

    cy.get(`a[href="/trust/form-amat/application-new-trust-name?appId=${applicationId}"]`).should('be.visible').contains('Add the trust')
    cy.get('h2[class=govuk-heading-l]').contains('Contributors')
    // cy.get('p').eq(3).contains('You can invite or remove contributors to this form. If you are not the chair of the school\'s governing body, you must add them so that they can submit this application.')

    return this
  }

  public FAMApplicationTrustNameComplete(applicationId: string): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Form a new multi-academy trust')

    cy.get('p[class="govuk-body govuk-radios__conditional"]').contains('All school and trust details must be given before this application can be submitted.')

    cy.get('.govuk-heading-m').eq(0).contains('Give details of schools in the trust')

    cy.get(`a[href="/school/school-overview?appId=${applicationId}&urn=113537"]`).contains('Plymstock School')
    cy.get('strong').eq(1).contains('Completed')

    cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).should('be.visible').contains('Add a school')
    cy.get(`a[href="/remove-school-selection?appId=${applicationId}"]`).contains('Remove a school')

    cy.get('.govuk-heading-m').eq(1).contains('Give details of the trust')

    cy.get('h3').contains('Plymouth')
    cy.get(`a[href="/trust/form-amat/application-new-trust-name?appId=${applicationId}"]`).contains('Change')

    cy.get(`a[href="/trust/form-amat/application-new-trust-summary?appId=${applicationId}"]`).contains('Trust details')
    cy.get('strong').eq(2).contains('In Progress')

    cy.get('h2[class=govuk-heading-l]').contains('Contributors')
    // cy.get('p').eq(3).contains('You can invite or remove contributors to this form. If you are not the chair of the school\'s governing body, you must add them so that they can submit this application.')

    return this
  }

  public FAMApplicationFuturePupilNumbersSubmittedElementsVisible(applicationId: string): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Form a new multi-academy trust')

    cy.get('p[class="govuk-body govuk-radios__conditional"]').contains('All school and trust details must be given before this application can be submitted.')

    cy.get('.govuk-heading-m').eq(0).contains('Give details of schools in the trust')

    cy.get(`a[href="/school/school-overview?appId=${applicationId}&urn=113537"]`).contains('Plymstock School')
    cy.get('strong').eq(1).contains('In Progress')

    cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).should('be.visible').contains('Add a school')
    cy.get(`a[href="/remove-school-selection?appId=${applicationId}"]`).contains('Remove a school')

    cy.get('.govuk-heading-m').eq(1).contains('Give details of the trust')

    cy.get(`a[href="/trust/form-amat/application-new-trust-name?appId=${applicationId}"]`).should('be.visible').contains('Add the trust')
    cy.get('h2[class=govuk-heading-l]').contains('Contributors')
    // cy.get('p').eq(3).contains('You can invite or remove contributors to this form. If you are not the chair of the school\'s governing body, you must add them so that they can submit this application.')

    return this
  }

  public selectFAMSchool(applicationId: string): this {
    cy.get(`a[href="/school/school-overview?appId=${applicationId}&urn=113537"]`).click()

    return this
  }

  public selectFAMAddTheTrust(applicationId: string): this {
    cy.get(`a[href="/trust/form-amat/application-new-trust-name?appId=${applicationId}"]`).click()

    return this
  }

  public selectFAMTrustDetails(applicationId: string): this {
    cy.get(`a[href="/trust/form-amat/application-new-trust-summary?appId=${applicationId}"]`).click()

    return this
  }

  public FAMApplicationOverviewCompleteElementsVisible(applicationId: string): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Form a new multi-academy trust')

    cy.get('.govuk-heading-m').eq(0).contains('Give details of schools in the trust')

    cy.get(`a[href="/school/school-overview?appId=${applicationId}&urn=113537"]`).contains('Plymstock School')
    cy.get('strong').eq(1).contains('Completed')

    cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).should('be.visible').contains('Add a school')
    cy.get(`a[href="/remove-school-selection?appId=${applicationId}"]`).contains('Remove a school')

    cy.get('.govuk-heading-m').eq(1).contains('Give details of the trust')

    cy.get('h3').contains('Plymouth')
    cy.get(`a[href="/trust/form-amat/application-new-trust-name?appId=${applicationId}"]`).contains('Change')

    cy.get(`a[href="/trust/form-amat/application-new-trust-summary?appId=${applicationId}"]`).contains('Trust details')
    cy.get('strong').eq(2).contains('Completed')

    cy.get('h2[class=govuk-heading-l]').contains('Contributors')
    // cy.get('p').eq(3).contains('You can invite or remove contributors to this form. If you are not the chair of the school\'s governing body, you must add them so that they can submit this application.')

    cy.get('input[type=submit]').should('be.visible').contains('Submit application')

    return this
  }
}

const yourApplication = new YourApplication()

export default yourApplication
