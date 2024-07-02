// TODO get better selectors
class Application {
  public checkTrustSectionComplete(): this {
    cy.get('strong[class="govuk-tag app-task-list__tag"]').contains('Completed')

    return this
  }

  public addTrust(): this {
    cy.get('[data-cy="applicationReference"]').then(($applicationId) => {
      const applicationId = $applicationId.text().split('_').pop().replace(/\s/g, '')
      cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${applicationId}"]`).click()
    })

    return this
  }

  public addSchool(): this {
    cy.get('[data-cy="applicationReference"]').then(($applicationId) => {
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
    cy.get('[data-cy="cancelApplication"]').click()

    return this
  }

  public inviteContributor(): this {
    cy.contains('invite or remove').click()

    return this
  }

  // Form A MAT specific
  public checkSchoolAdded(): this {
    cy.url().then((url) => {
      const applicationId = url.split('=').pop()

      cy.get(`a[href="/school/school-overview?appId=${applicationId}&urn=113537"]`).contains('Plymstock School')
      cy.get('strong').eq(1).contains('Not Started')
    })

    return this
  }

  public checkSchoolCompleted(): this {
    cy.get('strong').eq(1).contains('Completed')

    return this
  }

  public checkTrustNameCompleted(): this {
    cy.get('h3').contains('Plymouth')

    cy.get('strong').eq(2).contains('In Progress')

    return this
  }

  public checkSchoolStatus(): this {
    cy.get('strong').eq(1).contains('In Progress')

    return this
  }

  public selectFAMSchool(): this {
    cy.url().then((url) => {
      const applicationId = url.split('=').pop()
      cy.get(`a[href="/school/school-overview?appId=${applicationId}&urn=113537"]`).click()
    })

    return this
  }

  public addFAMTrust(): this {
    cy.url().then((url) => {
      const applicationId = url.split('=').pop()
      cy.get(`a[href="/trust/form-amat/application-new-trust-name?appId=${applicationId}"]`).click()
    })

    return this
  }

  public selectFAMTrust(): this {
    cy.url().then((url) => {
      const applicationId = url.split('=').pop()
      cy.get(`a[href="/trust/form-amat/application-new-trust-summary?appId=${applicationId}"]`).click()
    })

    return this
  }

  public checkApplicationOverviewCompleted(): this {
    cy.get('strong').eq(2).contains('Completed')

    return this
  }
}

const application = new Application()

export default application
