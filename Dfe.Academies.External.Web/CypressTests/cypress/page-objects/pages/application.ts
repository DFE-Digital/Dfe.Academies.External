class Application {
  public checkSectionComplete(section: string): this {
    switch (section) {
      case 'About the conversion':
        cy.get('[data-cy="sectionStatus"]').eq(0).contains('Completed')
        break
      case 'Further information':
        cy.get('[data-cy="sectionStatus"]').eq(1).contains('Completed')
        break
      case 'Finances':
        cy.get('[data-cy="sectionStatus"]').eq(2).contains('Completed')
        break
      case 'Future pupil numbers':
        cy.get('[data-cy="sectionStatus"]').eq(3).contains('Completed')
        break
      case 'Land and buildings':
        cy.get('[data-cy="sectionStatus"]').eq(4).contains('Completed')
        break
      case 'Consultation':
        cy.get('[data-cy="sectionStatus"]').eq(5).contains('Completed')
        break
	  case 'Conversion support grant':
        cy.get('[data-cy="sectionStatus"]').eq(6).contains('Completed')
        break
      case 'Declaration':
        cy.get('[data-cy="sectionStatus"]').eq(7).contains('Completed')
        break
      default:
        cy.log('Invalid option given')
        break
    }
    return this
  }

  public checkTrustSectionComplete(): this {
    cy.get('[data-cy="sectionStatus"]').contains('Completed')

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

  public startFurtherInformation(): this {
    cy.contains('Further information').click()

    return this
  }

  public startFinances(): this {
    cy.contains('Finances').click()

    return this
  }

  public startFuturePupilNumbers(): this {
    cy.contains('Future pupil numbers').click()

    return this
  }

  public startLandAndBuildings(): this {
    cy.contains('Land and buildings').click()

    return this
  }

  public startConsultation(): this {
    cy.contains('Consultation').click()

    return this
  }

  public startPreopeningSupportGrant(): this {
	  cy.contains('Conversion support grant').click()

    return this
  }

  public startDeclaration(): this {
    cy.contains('Declaration').click()

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

      cy.get(`a[href="/school/school-overview?appId=${applicationId}&urn=142116"]`).contains('Plymouth Studio School')
      cy.get('[data-cy="sectionStatus"]').eq(0).contains('Not Started')
    })

    return this
  }

  public checkSchoolCompleted(): this {
    cy.get('[data-cy="sectionStatus"]').eq(0).contains('Completed')

    return this
  }

  public checkTrustAdded(): this {
    cy.get('[data-cy="trustName"]').contains('Plymouth')

    cy.get('[data-cy="sectionStatus"]').eq(1).contains('In Progress')

    return this
  }

  public checkSchoolStatus(): this {
    cy.get('[data-cy="sectionStatus"]').eq(0).contains('In Progress')

    return this
  }

  public selectFAMSchool(): this {
    cy.url().then((url) => {
      const applicationId = url.split('=').pop()
      cy.get(`a[href="/school/school-overview?appId=${applicationId}&urn=142116"]`).click()
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
    cy.get('[data-cy="sectionStatus"]').eq(0).contains('Completed')
    cy.get('[data-cy="sectionStatus"]').eq(1).contains('Completed')

    return this
  }

  public applicationOverviewBanner(): this {
    cy.get('div[class="govuk-notification-banner"]')

    return this
  }
}

const application = new Application()

export default application
