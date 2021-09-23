import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  member! : Member;
  galleryOptions!: NgxGalleryOptions[];
  galleryImages!: NgxGalleryImage[];

  // add ActivatedRoute because when a user is clicked on, it will access and show who they are 
    // Provides info about route associated with the component
  constructor(private memberService : MembersService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMember();
  

  this.galleryOptions = [
    {
      width: '500px',
      height: '500px',
      imagePercent: 100,
      // number of image that appear below the main image
      thumbnailsColumns: 4,
      // Since its own library I dont understand fully
      imageAnimation: NgxGalleryAnimation.Slide,
      // Only lets them see the main image in the window not click on like instagram
      preview: false
    }
  ]
}

    //expected to return NgxGalleryImage array
  getImages(): NgxGalleryImage[] {
    const imageUrls = [];
    // Use for loop to go and get all member photos.
    for( const photo of this.member.photos ) 
    {
      imageUrls.push({
        // Seen when search for ngxGallery: if had different size for photos or different photos with different size, these 3 below could be used 
        // Use ? in case users dont have photos
        small: photo?.url,
        medium: photo?.url,
        big: photo?.url
      })
    }
    return imageUrls;
  }

  loadMember() {
    // Had to set members.service.ts getMember username parameter to type string | null, error if only set to string
    this.memberService.getMember(this.route.snapshot.paramMap.get('username')).subscribe(member => {
      this.member = member;
      // Load Photos after because you need to know when things are happening in angular - problems causes if statement below not loaded after like it is right now
      this.galleryImages = this.getImages();
    })
  }

}
