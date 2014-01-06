--アーティファクト－ラブリュス
function c80200016.initial_effect(c)
	--set
	local e1=Effect.CreateEffect(c)
	e1:SetType(EFFECT_TYPE_SINGLE)
	e1:SetCode(EFFECT_MONSTER_SSET)
	e1:SetValue(TYPE_SPELL)
	c:RegisterEffect(e1)
	--spsummon
	local e2=Effect.CreateEffect(c)
	e2:SetDescription(aux.Stringid(80200016,0))
	e2:SetCategory(CATEGORY_SPECIAL_SUMMON)
	e2:SetType(EFFECT_TYPE_SINGLE+EFFECT_TYPE_TRIGGER_F)
	e2:SetProperty(EFFECT_FLAG_DAMAGE_STEP)
	e2:SetCode(EVENT_TO_GRAVE)
	e2:SetCondition(c80200016.spcon)
	e2:SetTarget(c80200016.sptg)
	e2:SetOperation(c80200016.spop)
	c:RegisterEffect(e2)
	--special summon hand
	local e3=Effect.CreateEffect(c)
	e3:SetDescription(aux.Stringid(80200016,1))
	e3:SetCategory(CATEGORY_SPECIAL_SUMMON)
	e3:SetType(EFFECT_TYPE_FIELD+EFFECT_TYPE_TRIGGER_O)
	e3:SetProperty(EFFECT_FLAG_DAMAGE_STEP)
	e3:SetRange(LOCATION_HAND)
	e3:SetCode(EVENT_DESTROYED)
	e3:SetCondition(c80200016.spcon1)
	e3:SetTarget(c80200016.sptg1)
	e3:SetOperation(c80200016.spop)
	c:RegisterEffect(e3)
end
function c80200016.spcon(e,tp,eg,ep,ev,re,r,rp)
	local c=e:GetHandler()
	return 	c:IsPreviousLocation(LOCATION_SZONE) and c:IsPreviousPosition(POS_FACEDOWN) 
		and c:IsReason(REASON_DESTROY) and Duel.GetTurnPlayer()~=tp
end
function c80200016.sptg(e,tp,eg,ep,ev,re,r,rp,chk,chkc)
	if chk==0 then return true end
	Duel.SetOperationInfo(0,CATEGORY_SPECIAL_SUMMON,e:GetHandler(),1,0,0)
end
function c80200016.spop(e,tp,eg,ep,ev,re,r,rp)
	if e:GetHandler():IsRelateToEffect(e) then 
		Duel.SpecialSummon(e:GetHandler(),0,tp,tp,false,false,POS_FACEUP)
	end
end
function c80200016.cfilter(c,tp)
	return c:IsLocation(LOCATION_GRAVE) and c:IsControler(tp) and c:IsSetCard(0x97)
end
function c80200016.spcon1(e,tp,eg,ep,ev,re,r,rp)
	return eg:IsExists(c80200016.cfilter,1,nil,tp)
end
function c80200016.sptg1(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return e:GetHandler():IsRelateToEffect(e)
		and Duel.GetLocationCount(tp,LOCATION_MZONE)>0
		and e:GetHandler():IsCanBeSpecialSummoned(e,0,tp,false,false) end
	Duel.SetOperationInfo(0,CATEGORY_SPECIAL_SUMMON,e:GetHandler(),1,0,0)
end